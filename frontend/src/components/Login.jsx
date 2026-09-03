function Login({onLogin}){
    return(
        <div>
            <h1>Login</h1>
 
            <div class= "campo">
            <input type="email" name="email" id="" /><br />
            <input type="password" name="senha" id="" />
            </div>

            

            <button onClick={() => onLogin(true)}>Entrar</button>
        </div>
    )
}

export default Login