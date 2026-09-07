import { useState } from "react"

function Login({onLogin}){
    
    const[email, setEmail] = useState('');
    const[senha, setSenha] = useState('');
    
    return(
        <div>
            <h1>Login</h1>
 
            <div className= "campo">
            <input
             type="email"
             name="email" 
             onChange={(event) => setEmail(event.target.value)}/><br />

            <input
             type="password"
             name="senha"
             onChange={(event) => setSenha(event.target.value)}
              />
            </div>

            <button onClick={async() =>{

             const resposta = await fetch("http://localhost:5259/api/Usuario/login", {
             
                method: "POST",

                headers:{
                    "Content-Type": "application/json"
                },

                body: JSON.stringify({
                    email,
                    senha
                })
            })

            const dados = await resposta.json();

            console.log(dados);

            if(resposta.ok){
                onLogin(true)
            }
        }}>

            Entrar
            </button>
        
        </div>
    )
    
}


export default Login