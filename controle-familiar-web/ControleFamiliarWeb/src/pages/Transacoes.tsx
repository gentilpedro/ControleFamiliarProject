import { useEffect, useState } from "react";
import { api } from "../api/api";

import type { Transacao } from "../types/Transacao";
import type { Categoria } from "../types/Categoria";
import type { Pessoa } from "../types/Pessoa";

export default function Transacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<number>(0);
  const [tipo, setTipo] = useState(1);
  const [pessoaId, setPessoaId] = useState<number | "">("");
  const [categoriaId, setCategoriaId] = useState<number | "">("");
  const [mostrarForm, setMostrarForm] = useState(false);

  async function carregar() {
    const transacoesRes = await api.get("/transacoes");
    const pessoasRes = await api.get("/pessoas");
    const categoriasRes = await api.get("/categorias");

    setTransacoes(transacoesRes.data);
    setPessoas(pessoasRes.data);
    setCategorias(categoriasRes.data);
  }

  async function criarTransacao(e: React.FormEvent) {
    e.preventDefault();

    await api.post("/transacoes", {
      descricao,
      valor,
      tipo,
      pessoaId,
      categoriaId
    });

    setDescricao("");
    setValor(0);
    setTipo(1);
    setPessoaId("");
    setCategoriaId("");
    setMostrarForm(false);

    carregar();
  }

  function getTipoTexto(tipo: number) {
    return tipo === 1 ? "Receita" : "Despesa";
  }

  function getTipoClasse(tipo: number) {
    return tipo === 1 ? "badge badge-receita" : "badge badge-despesa";
  }

  useEffect(() => {
    carregar();
  }, []);

  return (
    <>
      <div className="page-header">
        <div>
          <h1 className="page-title">Transações</h1>
          <p className="page-subtitle">Cadastre e acompanhe todas as movimentações financeiras.</p>
        </div>

        <button
          className="btn btn-primary"
          onClick={() => setMostrarForm(!mostrarForm)}
        >
          {mostrarForm ? "Fechar" : "Nova Transação"}
        </button>
      </div>

      {mostrarForm && (
        <div className="card">
          <form onSubmit={criarTransacao} className="form-row">
            <input
              className="input"
              placeholder="Descrição"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
            />

            <input
              className="input"
              type="number"
              placeholder="Valor"
              value={valor}
              onChange={(e) => setValor(Number(e.target.value))}
            />

            <select
              className="select"
              value={tipo}
              onChange={(e) => setTipo(Number(e.target.value))}
            >
              <option value={1}>Receita</option>
              <option value={2}>Despesa</option>
            </select>

            <select
              className="select"
              value={pessoaId}
              onChange={(e) => setPessoaId(Number(e.target.value))}
            >
              <option value="">Selecionar Pessoa</option>
              {pessoas.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.nome}
                </option>
              ))}
            </select>

            <select
              className="select"
              value={categoriaId}
              onChange={(e) => setCategoriaId(Number(e.target.value))}
            >
              <option value="">Selecionar Categoria</option>
              {categorias.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.descricao}
                </option>
              ))}
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
                <th>Valor</th>
                <th>Tipo</th>
                <th>Pessoa</th>
                <th>Categoria</th>
              </tr>
            </thead>
            <tbody>
              {transacoes.map((t) => (
                <tr key={t.id}>
                  <td>{t.id}</td>
                  <td>{t.descricao}</td>
                  <td>
                    {Number(t.valor).toLocaleString("pt-BR", {
                      style: "currency",
                      currency: "BRL"
                    })}
                  </td>
                  <td>
                    <span className={getTipoClasse(t.tipo)}>
                      {getTipoTexto(t.tipo)}
                    </span>
                  </td>
                  <td>{t.pessoa}</td>
                  <td>{t.categoria}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );

}