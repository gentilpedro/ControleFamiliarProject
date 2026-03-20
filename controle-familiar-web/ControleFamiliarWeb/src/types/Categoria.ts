export interface Categoria {
  id: number;
  descricao: string;
  finalidade: number;
}

export interface CategoriaResumo{
  categoria: string;
  total: number;
}