import { BrowserRouter, Routes, Route } from "react-router-dom";

import Layout from "../components/Layout";

import Home from "../pages/Home";
import Pessoas from "../pages/Pessoas";
import Categorias from "../pages/Categorias";
import Transacoes from "../pages/Transacoes";
import Relatorios from "../pages/Relatorio";

export default function AppRoutes() {

  return (

    <BrowserRouter>

      <Routes>

        <Route path="/" element={<Layout />}>

          <Route index element={<Home />} />

          <Route path="pessoas" element={<Pessoas />} />

          <Route path="categorias" element={<Categorias />} />

          <Route path="transacoes" element={<Transacoes />} />

          <Route path="relatorios" element={<Relatorios />} />

        </Route>

      </Routes>

    </BrowserRouter>

  );

}