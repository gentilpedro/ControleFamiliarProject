import { useEffect, useState } from "react";
import { api } from "../api/api";
import type { Relatorio } from "../types/Relatorios";
import type { CategoriaResumo } from "../types/Categoria";

import {
  PieChart,
  Pie,
  Cell,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  Legend,
  CartesianGrid,
  ResponsiveContainer
} from "recharts";

export default function Relatorios() {

  const [relatorio, setRelatorio] = useState<Relatorio>();
  const [categorias, setCategorias] = useState<CategoriaResumo[]>([]);

  async function carregar() {

    const pessoasResponse = await api.get("/relatorios/totais-por-pessoa");
    const categoriasResponse = await api.get("/relatorios/totais-por-categoria");

    setRelatorio(pessoasResponse.data);

    const categoriasFormatadas = categoriasResponse.data.map((c:any)=>({
      categoria: c.categoria,
      total: c.total
    }));

    setCategorias(categoriasFormatadas);

  }

  useEffect(() => {
    carregar();
  }, []);

  if (!relatorio) return <p>Carregando...</p>;

  const cores = [
    "#ff9800",
    "#2196f3",
    "#4caf50",
    "#f44336",
    "#9c27b0"
  ];
async function baixarExcelPessoa() {

  const response = await api.get("/relatorios/excel-pessoa", {
    responseType: "blob"
  });

  const url = window.URL.createObjectURL(new Blob([response.data]));

  const link = document.createElement("a");

  link.href = url;
  link.setAttribute("download", "relatorio-pessoas.xlsx");

  document.body.appendChild(link);

  link.click();

  link.remove();
}

async function baixarExcelCategoria() {

  const response = await api.get("/relatorios/excel-categoria", {
    responseType: "blob"
  });

  const url = window.URL.createObjectURL(new Blob([response.data]));

  const link = document.createElement("a");

  link.href = url;
  link.setAttribute("download", "relatorio-categorias.xlsx");

  document.body.appendChild(link);

  link.click();

  link.remove();
}

const saldoLiquido = relatorio.totalReceitas - relatorio.totalDespesas;

return (
  <>
    <div className="page-header">
      <div>
        <h1 className="page-title">Dashboard Financeiro</h1>
        <p className="page-subtitle">Acompanhe receitas, despesas e saldo geral.</p>
      </div>
    </div>

    <div className="summary-grid">
      <div className="summary-card">
        <div className="summary-label">Receitas</div>
        <div className="summary-value receita">
          {relatorio.totalReceitas.toLocaleString("pt-BR", {
            style: "currency",
            currency: "BRL"
          })}
        </div>
      </div>

      <div className="summary-card">
        <div className="summary-label">Despesas</div>
        <div className="summary-value despesa">
          {relatorio.totalDespesas.toLocaleString("pt-BR", {
            style: "currency",
            currency: "BRL"
          })}
        </div>
      </div>

      <div className="summary-card">
        <div className="summary-label">Saldo Líquido</div>
        <div className="summary-value saldo">
          {saldoLiquido.toLocaleString("pt-BR", {
            style: "currency",
            currency: "BRL"
          })}
        </div>
      </div>
    </div>
    <div className="Section-DespXPessoa">
        <button className="btn btn-danger btn-excel"
             onClick={baixarExcelPessoa}>
        Exportar Excel
      </button>
      <div className="chart-card">
        <h2 className="section-title">Receitas vs Despesas por Pessoa</h2>
        <ResponsiveContainer width="100%" height={320}>
          <BarChart data={relatorio.pessoas}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="pessoa" />
            <YAxis />
            <Tooltip />
            <Legend />
            <Bar dataKey="totalReceitas" fill="#16a34a" name="Receitas" />
            <Bar dataKey="totalDespesas" fill="#dc2626" name="Despesas" />
          </BarChart>
        </ResponsiveContainer>
      </div>
    </div>
  
    
    <div className="section-DespXCateg">
         <button className="btn btn-danger btn-excel"
             onClick={baixarExcelCategoria}>
        Exportar Excel
      </button>
    <div className="chart-card">
      <h2 className="section-title">Despesas por Categoria</h2>
      <ResponsiveContainer width="100%" height={320}>
        <PieChart>
          <Pie
            data={categorias}
            dataKey="total"
            nameKey="categoria"
            cx="50%"
            cy="50%"
            outerRadius={110}
            label
          >
            {categorias.map((entry, index) => (
              <Cell key={index} fill={cores[index % cores.length]} />
            ))}
          </Pie>
          <Tooltip />
          <Legend />
        </PieChart>
      </ResponsiveContainer>
    </div>
    </div>
  </>
);
}