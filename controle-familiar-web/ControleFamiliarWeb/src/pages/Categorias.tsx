import { useEffect, useState } from "react";
import { api } from "../api/api";
import type { Categoria } from "../types/Categoria";

export default function Categorias() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [descricao, setDescricao] = useState("");
  const [finalidade, setFinalidade] = useState(1);
  const [mostrarForm, setMostrarForm] = useState(false);

  async function carregar() {
    const response = await api.get("/categorias");
    setCategorias(response.data);
  }

  async function criarCategoria(e: React.FormEvent) {
    e.preventDefault();

    await api.post("/categorias", {
      descricao,
      finalidade
    });

    setDescricao("");
    setFinalidade(1);
    setMostrarForm(false);
    carregar();
  }

  function getFinalidadeTexto(finalidade: number) {
    if (finalidade === 1) return "Receita";
    if (finalidade === 2) return "Despesa";
    return "Ambas";
  }

  function getFinalidadeClasse(finalidade: number) {
    if (finalidade === 1) return "badge badge-receita";
    if (finalidade === 2) return "badge badge-despesa";
    return "badge badge-ambas";
  }

  useEffect(() => {
    carregar();
  }, []);

  return (
    <>
      <div className="page-header">
        <div>
          <h1 className="page-title">Categorias</h1>
          <p className="page-subtitle">Organize receitas e despesas por categoria.</p>
        </div>

        <button
          className="btn btn-primary"
          onClick={() => setMostrarForm(!mostrarForm)}
        >
          {mostrarForm ? "Fechar" : "Nova Categoria"}
        </button>
      </div>

      {mostrarForm && (
        <div className="card">
          <form onSubmit={criarCategoria} className="form-row categorias">
            <input
              className="input"
              placeholder="Descrição da categoria"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
            />

            <select
              className="select"
              value={finalidade}
              onChange={(e) => setFinalidade(Number(e.target.value))}
            >
              <option value={1}>Receita</option>
              <option value={2}>Despesa</option>
              <option value={3}>Ambas</option>
            </select>

            <button className="btn btn-success" type="submit">
              Salvar
            </button>

            <button
              className="btn btn-secondary"
              type="button"
              onClick={() => setMostrarForm(false)}
            >
              Cancelar
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <div className="table-wrapper">
          <table className="table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Descrição</th>
                <th>Finalidade</th>
              </tr>
            </thead>
            <tbody>
              {categorias.map((c) => (
                <tr key={c.id}>
                  <td>{c.id}</td>
                  <td>{c.descricao}</td>
                  <td>
                    <span className={getFinalidadeClasse(c.finalidade)}>
                      {getFinalidadeTexto(c.finalidade)}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}