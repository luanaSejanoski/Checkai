import './App.css'
import Sidebar from './components/Sidebar'
import Dashboard from './components/Dashboard'
import MeusHabitos from './components/MeusHabitos'
import Login from './components/Login'
import { useState } from 'react'

function App({nome, email}) {
const [logado, setLogado] = useState(false)
const [pagina, setPagina] = useState("dashboard")

    if(!logado){
      return <Login onLogin={setLogado}/>

    }
      return <div className="app">
      <Sidebar setPagina={setPagina}/>
      {pagina === "dashboard" && <Dashboard />}
      {pagina === "habitos" && <MeusHabitos />}
      
</div>
  
}
export default App