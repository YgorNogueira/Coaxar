using CoaxarApp.Models;

namespace CoaxarApp.Services;

public static class PropostaPsicopedagogicaRepository
{
    private static readonly IReadOnlyList<PropostaPsicopedagogicaModel> Propostas =
    [
        new()
        {
            Id = "ensino-fundamental-ii",
            Nivel = "Ensino Fundamental II",
            Duracao = "50 a 60 minutos",
            Area = "Ciências da Natureza",
            Resumo = "Exploração de morfologia, habitat e vocalizações de anuros com apoio do aplicativo e atividades sensoriais.",
            HabilidadesBncc =
            [
                "EF03CI01 – Produção de sons a partir da vibração de variados objetos.",
                "EF03CI03 – Saúde auditiva e ambiental.",
                "EF03CI04 – Identificação de características sobre o modo de vida dos animais.",
                "EF02CI04 – Descrição de plantas e animais e relação com o ambiente.",
                "EF03CI06 – Comparar animais e organizar grupos com base em características externas."
            ],
            Objetivos =
            [
                "Reconhecer características morfológicas de diferentes espécies de anuros.",
                "Explorar os sons produzidos por esses animais, relacionando-os a seus hábitos e ambientes.",
                "Analisar padrões de comportamento e habitat com base em observações visuais e sonoras.",
                "Promover experiências sensoriais integradas usando artefatos que permitem percepção de vibração dos sons."
            ],
            Recursos =
            [
                "Aplicativo com banco de dados de anuros, incluindo imagens, sons e descrições morfológicas.",
                "Tablets ou computadores com acesso ao aplicativo.",
                "Alto-falantes ou caixas de som portáteis.",
                "Artefatos que transmitem vibração sonora, como superfícies metálicas, caixas ressonantes, madeira ou balões.",
                "Quadro ou folhas para registro de observações.",
                "Fichas de espécies com imagens, descrição morfológica e espaço para anotações."
            ],
            Etapas =
            [
                new()
                {
                    Duracao = "5–10 min",
                    Titulo = "Introdução",
                    Descricao = "Apresentar o aplicativo aos alunos, explorar a interface e destacar imagens dos animais e suas principais características morfológicas. Em seguida, discutir os ambientes em que os anuros vivem, relacionando coloração, tamanho e padrão de pele aos habitats ocupados."
                },
                new()
                {
                    Duracao = "15–20 min",
                    Titulo = "Observação e exploração",
                    Descricao = "Dividir os alunos em grupos para escolher uma espécie no aplicativo, observar imagens e registrar cor, tamanho aproximado e padrões de pele. Os grupos também ouvem os sons das espécies e experimentam artefatos de vibração para perceber as ondas sonoras de forma sensorial."
                },
                new()
                {
                    Duracao = "10–15 min",
                    Titulo = "Análise e comparação",
                    Descricao = "Comparar os padrões observados entre espécies e discutir como habitat, vegetação, corpos d’água e características ambientais podem influenciar a produção sonora. Relacionar as variações dos sons a comunicação, defesa territorial e atração de parceiros reprodutivos."
                },
                new()
                {
                    Duracao = "5–10 min",
                    Titulo = "Registro e discussão",
                    Descricao = "Preencher fichas com anotações sobre morfologia, ambiente e sons. Depois, compartilhar as descobertas com a turma, destacando relações entre forma, habitat e vocalização."
                }
            ],
            Avaliacao =
            [
                "Participação na exploração do aplicativo e nos experimentos com vibração.",
                "Fichas de registro completas com observações corretas de morfologia e sons.",
                "Relatório oral ou resumo escrito comparando espécies e destacando relações habitat–som–comportamento."
            ]
        },
        new()
        {
            Id = "ensino-medio",
            Nivel = "Ensino Médio",
            Duracao = "50 a 60 minutos",
            Area = "Ciências da Natureza",
            Resumo = "Análise de bioacústica, comportamento, ecologia e impactos ambientais usando registros digitais e discussão em grupo.",
            HabilidadesBncc =
            [
                "EM13CNT102 – Uso de tecnologias digitais para explorar sistemas e fenômenos.",
                "EM13CNT105 – Analisar ciclos biogeoquímicos e impactos ambientais.",
                "EM13CNT203 – Avaliar efeitos de intervenções nos ecossistemas utilizando simulações.",
                "EM13CNT302 – Comunicar resultados usando diferentes linguagens e mídias digitais.",
                "EM13CNT303 – Interpretar textos e dados de divulgação científica.",
                "EM13CNT306 – Avaliar riscos e utilizar recursos tecnológicos para simulações.",
                "EM13CNT307 – Analisar propriedades de materiais para adequação de uso em diferentes contextos."
            ],
            Objetivos =
            [
                "Explorar imagens e características morfológicas de diferentes espécies de anuros.",
                "Analisar os chamados acústicos das espécies, relacionando-os a comportamento, habitat e ecologia.",
                "Aplicar conceitos de bioacústica utilizando o aplicativo e experimentos com vibração.",
                "Registrar e comunicar resultados das observações de forma digital e visual.",
                "Relacionar impactos ambientais com mudanças em padrões acústicos das espécies."
            ],
            Recursos =
            [
                "Aplicativo de bioacústica com imagens, descrições e sons dos anuros.",
                "Tablets ou computadores com acesso ao aplicativo.",
                "Alto-falantes portáteis e superfícies que transmitam vibração, como caixas ressonantes, madeira e metais.",
                "Fichas de registro para observações de morfologia, habitat e som.",
                "Quadro ou projetor para discussão coletiva.",
                "Recursos digitais para simulação de ambientes sonoros e análise de frequência."
            ],
            Etapas =
            [
                new()
                {
                    Duracao = "5–10 min",
                    Titulo = "Introdução",
                    Descricao = "Apresentar o aplicativo, destacando acesso a imagens, sons e informações morfológicas das espécies. Discutir a importância ecológica dos anuros e fatores que afetam seus chamados, como alteração de habitat, poluição sonora, mudanças climáticas e degradação de ambientes aquáticos."
                },
                new()
                {
                    Duracao = "15–20 min",
                    Titulo = "Observação e exploração",
                    Descricao = "Dividir os alunos em grupos para escolher uma espécie e registrar coloração, tamanho, formato corporal, padrão de pele e outras estruturas visíveis. Os grupos ouvem os chamados acústicos e usam artefatos de vibração para aproximar escuta e experiência sensorial."
                },
                new()
                {
                    Duracao = "10–15 min",
                    Titulo = "Análise e comparação",
                    Descricao = "Comparar vocalizações entre espécies, discutindo duração, intensidade, ritmo e percepção dos sons. Relacionar chamados a atração de parceiros, defesa territorial e comunicação, conectando comportamento, habitat e impactos ambientais por meio de simulações digitais quando possível."
                },
                new()
                {
                    Duracao = "5–10 min",
                    Titulo = "Registro e discussão",
                    Descricao = "Preencher fichas digitais ou físicas sobre morfologia, habitat e sons. A partir dos registros, criar gráficos, esquemas ou representações visuais e apresentar as descobertas para a turma, destacando relações entre ambiente, comportamento e vocalização."
                }
            ],
            Avaliacao =
            [
                "Participação e engajamento nas atividades de exploração e experimentação.",
                "Qualidade das fichas de registro com informações precisas de morfologia, habitat e sons.",
                "Capacidade de relacionar observações a conceitos ecológicos e de bioacústica.",
                "Clareza e organização na apresentação de resultados digitais ou gráficos."
            ]
        }
    ];

    public static IReadOnlyList<PropostaPsicopedagogicaModel> GetAll() => Propostas;

    public static PropostaPsicopedagogicaModel? GetById(string id) =>
        Propostas.FirstOrDefault(proposta =>
            proposta.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
}
