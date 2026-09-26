import { useState } from "react";

function Perfil(){

    const [editando, setEditando] = useState(false);
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");

    return(
        <div className="perfil-container">

               <div className="perfil-card">

                <div className="foto-perfil">
                    <span>L</span>
                </div>

                <label>Nome:</label><br />
                <input
                    type="text"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    readOnly={!editando}
                /><br /><br />
                

                <label>E-mail:</label><br />
                <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    readOnly={!editando}
                /><br /><br />

                {editando ? (
                    <>
                        <button className="perfil-salvar">
                            Salvar
                        </button>

                        <button onClick={() => setEditando(false)}
                         className="perfil-cancelar"
                         >
                            Cancelar
                        </button>
                    </>
                ) : (
                    <button
                        onClick={() => setEditando(true)}
                        className="editar-perfil"
                    >
                        Editar Perfil
                    </button>
                )}

            </div>

            <div className="jornada-card">

                <div className="cards-jornada">
                <h2>📊 Minha jornada</h2>

                <div className="estatisticas">

                    <div className="estatistica">
                        <span className="icone-estatistica">✓</span><br /><br />
                        <strong>0</strong><br />
                        <p>Hábitos criados</p>
                    </div>

                    <div className="estatistica">
                        <span className="icone-estatistica">◎</span><br /><br />
                        <strong>0</strong><br />
                        <p>Metas concluídas</p>
                    </div>

                    <div className="estatistica">
                        <span className="icone-estatistica">▣</span><br /><br />
                        <strong>0</strong><br />
                        <p>Dias registrados</p>
                    </div>
                </div>
                </div>

            </div>

        </div>
    );
}

export default Perfil;