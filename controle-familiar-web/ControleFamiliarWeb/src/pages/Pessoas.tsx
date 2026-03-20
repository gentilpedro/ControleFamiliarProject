import { useEffect, useState } from "react";
import { api } from "../api/api";
import type { Pessoa } from "../types/Pessoa";
import Modal from "../components/Modal";

export default function Pessoas() {
 const [pessoas,setPessoas] = useState<Pessoa[]>([])

  const [editModal,setEditModal] = useState(false)
  const [deleteModal,setDeleteModal] = useState(false)

  const [pessoaAtual,setPessoaAtual] = useState<Pessoa | null>(null)

  const [nome,setNome] = useState("")
  const [idade,setIdade] = useState<number>(0)

  const [mostrarForm, setMostrarForm] = useState(false);

  async function carregar() {
    const response = await api.get("/pessoas");
    setPessoas(response.data);
  }

  function abrirEditar(pessoa: Pessoa) {
    setPessoaAtual(pessoa);
    setNome(pessoa.nome);
    setIdade(pessoa.idade);
    setEditModal(true);
  }
  
async function salvarEdicao(){

  if(!pessoaAtual) return

  await api.patch(`/pessoas/${pessoaAtual.id}`,{

    nome,
    idade

  })

  setEditModal(false)

  carregar()

}
  async function criarPessoa(e: React.FormEvent) {
    e.preventDefault();

    await api.post("/pessoas", {
      nome,
      idade
    });

    setNome("");
    setIdade(0);
    setMostrarForm(false);
    carregar();
  }

   function abrirDelete(p:Pessoa){

    setPessoaAtual(p)

    setDeleteModal(true)

  }

  async function confirmarDelete(){

    if(!pessoaAtual) return

    await api.delete(`/pessoas/${pessoaAtual.id}`)

    setDeleteModal(false)

    carregar()

  }

  useEffect(() => {
    carregar();
  }, []);

  return (
    <>
      <div className="page-header">
        <div>
          <h1 className="page-title">Pessoas</h1>
          <p className="page-subtitle">Gerencie as pessoas cadastradas no sistema.</p>
        </div>

        <button
          className="btn btn-primary"
          onClick={() => setMostrarForm(!mostrarForm)}
        >
          {mostrarForm ? "Fechar" : "Nova Pessoa"}
        </button>
      </div>

      {mostrarForm && (
        <div className="card">
          <form onSubmit={criarPessoa} className="form-row pessoas">
            <input
              className="input"
              placeholder="Nome da pessoa"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
            />

            <input
              className="input"
              type="number"
              placeholder="Idade"
              value={idade}
              onChange={(e) => setIdade(Number(e.target.value))}
            />

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
                <th>Nome</th>
                <th>Idade</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {pessoas.map((p) => (
                <tr key={p.id}>
                  <td>{p.id}</td>
                  <td>{p.nome}</td>
                  <td>{p.idade} anos</td>
                  <td className="table-actions">
                    <button
                      className="btn btn-success icon-btn"
                      onClick={()=>abrirEditar(p)}>
                      ✏ 
                    </button> 
                    
                    <button
                      className="btn btn-danger icon-btn"
                      onClick={()=>abrirDelete(p)}>
                      🗑
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      
      {/* MODAL EDITAR */}

      <Modal
        open={editModal}
        title="Editar Pessoa"
        onClose={()=>setEditModal(false)}
      >

        <input
          className="input"
          value={nome}
          onChange={(e)=>setNome(e.target.value)}
        />

        <input
          className="input"
          type="number"
          value={idade}
          onChange={(e)=>setIdade(Number(e.target.value))}
        />

        <button
          className="btn btn-success"
          onClick={salvarEdicao}
        >
          Salvar
        </button>

      </Modal>


      {/* MODAL DELETE */}

      <Modal
        open={deleteModal}
        title="Confirmar Exclusão"
        onClose={()=>setDeleteModal(false)}
      >

        <p>

          Deseja realmente excluir <b>{pessoaAtual?.nome}</b>?

        </p>

        <button
          className="btn btn-danger"
          onClick={confirmarDelete}
        >
          Excluir
        </button>

      </Modal>

    </div>
    </>
  );

}