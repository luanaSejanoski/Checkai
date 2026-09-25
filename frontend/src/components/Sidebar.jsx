import './Sidebar.css';

function Sidebar({ setPagina }) {
  return (
    <aside>
      <h2>Checkaí</h2>

      <nav>
        <ul>
            <li onClick={() => setPagina("dashboard")}>🏠 Dashboard</li>            
            <li onClick={() => setPagina("habitos")}>📝 Meus hábitos</li>
            <li onClick={() => setPagina("historico")}>📅 Histórico</li>
            <li onClick={() => setPagina("perfil")}>👤 Perfil</li>
            <li>🚪 Sair</li>
        </ul>
      </nav>
    </aside>
  )
}

export default Sidebar