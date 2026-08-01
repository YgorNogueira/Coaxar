namespace CoaxarApp.Controls;

//Container que permite dar zoom na imagem com pinça, arrastar e duplo toque
public class PinchZoomContainer : ContentView
{
    //Limites de zoom
    private const double MinScale = 1;
    private const double MaxScale = 4;

    //Zoom aplicado pelo duplo toque
    private const double DoubleTapScale = 2.5;

    private double _currentScale = 1;
    private double _startScale = 1;
    private double _xOffset;
    private double _yOffset;

    public PinchZoomContainer()
    {
        var pinch = new PinchGestureRecognizer();
        pinch.PinchUpdated += OnPinchUpdated;
        GestureRecognizers.Add(pinch);

        var pan = new PanGestureRecognizer();
        pan.PanUpdated += OnPanUpdated;
        GestureRecognizers.Add(pan);

        var doubleTap = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
        doubleTap.Tapped += OnDoubleTapped;
        GestureRecognizers.Add(doubleTap);
    }

    //Volta a imagem ao estado original, sem zoom
    public void Reset()
    {
        _currentScale = 1;
        _startScale = 1;
        _xOffset = 0;
        _yOffset = 0;

        if (Content is null)
            return;

        Content.AnchorX = 0;
        Content.AnchorY = 0;
        Content.Scale = 1;
        Content.TranslationX = 0;
        Content.TranslationY = 0;
    }

    //Zoom com o gesto de pinça, mantendo o ponto apertado sob os dedos
    private void OnPinchUpdated(object? sender, PinchGestureUpdatedEventArgs e)
    {
        if (Content is null)
            return;

        if (e.Status == GestureStatus.Started)
        {
            _startScale = Content.Scale;
            Content.AnchorX = 0;
            Content.AnchorY = 0;
        }

        if (e.Status == GestureStatus.Running)
        {
            _currentScale += (e.Scale - 1) * _startScale;
            _currentScale = Math.Clamp(_currentScale, MinScale, MaxScale);

            var renderedX = Content.X + _xOffset;
            var deltaX = renderedX / Width;
            var deltaWidth = Width / (Content.Width * _startScale);
            var originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

            var renderedY = Content.Y + _yOffset;
            var deltaY = renderedY / Height;
            var deltaHeight = Height / (Content.Height * _startScale);
            var originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

            var targetX = _xOffset - (originX * Content.Width) * (_currentScale - _startScale);
            var targetY = _yOffset - (originY * Content.Height) * (_currentScale - _startScale);

            Content.TranslationX = Math.Clamp(targetX, -Content.Width * (_currentScale - 1), 0);
            Content.TranslationY = Math.Clamp(targetY, -Content.Height * (_currentScale - 1), 0);
            Content.Scale = _currentScale;
        }

        if (e.Status == GestureStatus.Completed)
        {
            _xOffset = Content.TranslationX;
            _yOffset = Content.TranslationY;
        }
    }

    //Arrasta a imagem quando está com zoom
    private void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        if (Content is null || _currentScale <= 1)
            return;

        switch (e.StatusType)
        {
            case GestureStatus.Running:
                Content.TranslationX = Math.Clamp(
                    _xOffset + e.TotalX, -Content.Width * (_currentScale - 1), 0);
                Content.TranslationY = Math.Clamp(
                    _yOffset + e.TotalY, -Content.Height * (_currentScale - 1), 0);
                break;

            case GestureStatus.Completed:
                _xOffset = Content.TranslationX;
                _yOffset = Content.TranslationY;
                break;
        }
    }

    //Duplo toque: aproxima no ponto tocado ou volta ao normal
    private void OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (Content is null)
            return;

        if (_currentScale > 1)
        {
            Reset();
            return;
        }

        var tapPoint = e.GetPosition(Content)
            ?? new Point(Content.Width / 2, Content.Height / 2);

        Content.AnchorX = 0;
        Content.AnchorY = 0;

        _currentScale = DoubleTapScale;
        _xOffset = Math.Clamp(
            Width / 2 - tapPoint.X * _currentScale, -Content.Width * (_currentScale - 1), 0);
        _yOffset = Math.Clamp(
            Height / 2 - tapPoint.Y * _currentScale, -Content.Height * (_currentScale - 1), 0);

        Content.Scale = _currentScale;
        Content.TranslationX = _xOffset;
        Content.TranslationY = _yOffset;
    }
}
