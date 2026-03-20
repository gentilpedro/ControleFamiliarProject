import { Link } from "react-router-dom";

export default function Navbar() {

  return (
    <nav style={{
      display: "flex",
      gap: "20px",
      padding: "20px",
      backgroundColor: "#282c34",
      color: "white"
    }}>

      <Link to="/" style={{color:"white"}}>Pessoas</Link>
      <Link to="/categorias" style={{color:"white"}}>Categorias</Link>
      <Link to="/transacoes" style={{color:"white"}}>Transações</Link>
      <Link to="/relatorios" style={{color:"white"}}>Relatórios</Link>

    </nav>
  );

}