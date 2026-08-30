import CardResumo from './CardResumo'
import HabitoCard from './HabitoCard'

function Dashboard() {
  const habitos =[
    {
        nome: 'Caminhar',
        descricao: '30 minutos'
    },
    {
        nome: 'Estudar C#',
        descricao: '1 hora'
    },
    {
        nome: 'Beber água',
        descricao: '2L'
    }
  ]
  return (
    <main>
      <h1>Olá! 👋</h1>
      <p>Aqui está o resumo dos seus hábitos.</p>

      <div className="cards">
        <CardResumo
          titulo="Hábitos ativos"
          valor="5"
          icone="📝"
        />

        <CardResumo
          titulo="Concluídos hoje"
          valor="3"
          icone="✅"
        />

        <CardResumo
          titulo="Sequência atual"
          valor="7 dias"
          icone="🔥"
        />
      </div>

        <h2>Hábitos de hoje</h2>
        
    {habitos.map((habito, index) => (
    <HabitoCard
        key={index}
        nome={habito.nome}
        descricao={habito.descricao}
    />
))}

    </main>
  )
}

export default Dashboard