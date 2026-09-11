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
    buscarMaiorSequencia()

  }, [])

  async function buscarMaiorSequencia() {
      const token =localStorage.getItem("token");

      const resposta = await fetch("http://localhost:5259/api/Habitos/maior-sequencia",{

      method: "GET",

      headers:{
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      }

      });

      const dados = await resposta.json()
      setMaiorSequencia(dados);
}


  const [habitos, setHabitos] = useState([])
  const [logs, setLogs] = useState([])
  const [maiorSequencia, setMaiorSequencia] = useState(0);


  function atualizarLogs(habitoId, concluido) {
    if(concluido){

      setLogs([...logs, 
        {
          habitoId: habitoId,
          concluido: concluido
        }])
      }else{
        setLogs(
          logs.filter(log => log.habitoId != habitoId)
        )
      }
  }

  return (
    <main>
      <h1>Olá! 👋</h1>
      <p>Aqui está o resumo dos seus hábitos.</p>

      <div className="cards">
        <CardResumo
          titulo="Hábitos ativos"
          valor={habitos.length}
          icone="📝"
        />

        <CardResumo
          titulo="Concluídos hoje"
          valor= {logs.filter(log => log.concluido).length}
          icone="✅"
        />

        <CardResumo
          titulo="Maior Sequência"
          valor={maiorSequencia}
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
            atualizarLogs={atualizarLogs}
            buscarMaiorSequencia={buscarMaiorSequencia}
          />
        )
      })}
    </main>
  )
}

export default Dashboard