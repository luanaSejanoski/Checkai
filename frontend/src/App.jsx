import './App.css'
import Sidebar from './components/Sidebar'
import Dashboard from './components/Dashboard'
import Login from './components/Login'
import { useState } from 'react'

function App({nome, email}) {
  const [logado, setLogado] = useState(false)
    if(!logado){
      return <Login onLogin={setLogado}/>

    }
      return <div className="app">
      <Sidebar />
      <Dashboard />
</div>
  
}
export default App