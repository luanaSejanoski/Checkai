import CardResumo from './CardResumo'
import HabitoCard from './HabitoCard'
import { useEffect, useState } from 'react'

function Dashboard() {
  useEffect(() => {

    async function buscarHabitos() {
      const token = localStorage.getItem("token")

      const resposta = await fetch("http://localhost:5259/api/Habitos", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        }
      })

      const dados = await resposta.json()
      setHabitos(dados)
    }

    async function buscarLogs() {
      const token = localStorage.getItem("token")

      const resposta = await fetch("http://localhost:5259/api/HabitosLog", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        }
      })

      console.log(resposta.status)

      const dados = await resposta.json()
      console.log(dados)

      setLogs(dados)
    }

    buscarHabitos()
    buscarLogs()

  }, [])

  const [habitos, setHabitos] = useState([])
  const [logs, setLogs] = useState([])

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

      {habitos.map((habito) => {
        const concluidoHoje = logs.some(
          log => log.habitoId == habito.id
        )

        console.log(habito.nome, concluidoHoje)

        return (
          <HabitoCard
            key={habito.id}
            id={habito.id}
            nome={habito.nome}
            descricao={habito.descricao}
            concluido={concluidoHoje}
          />
        )
      })}
    </main>
  )
}

export default Dashboard