import type { PessoaResumo } from './PessoaResumo.ts';

export interface Relatorio {
  pessoas: PessoaResumo[];
  totalReceitas: number;
  totalDespesas: number;
  saldoLiquido: number;
}
