import './App.css'
import Sidebar from './components/Sidebar'
import Dashboard from './components/Dashboard'
import MeusHabitos from './components/MeusHabitos'
import Historico from './components/Historico'
import Login from './components/Login'
import { useState } from 'react'

function App({nome, email}) {
const [logado, setLogado] = useState(false)
const [pagina, setPagina] = useState("dashboard")
const [historico, setHistorico] = useState("historico")

    if(!logado){
      return <Login onLogin={setLogado}/>

    }
      return <div className="app">
  <Sidebar setPagina={setPagina} />

  <main className="conteudo">
    {pagina === "dashboard" && <Dashboard />}
    {pagina === "habitos" && <MeusHabitos />}
    {pagina === "historico" && <Historico/>}
  </main>
</div>
  
}
export default App