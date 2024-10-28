import { ModeloEnum } from "./modelo";

export interface Caminhao {
    id: number;
    modelo: ModeloEnum;
    anoFabricacao: number;
    codigoChassi: string;
    cor: string;
    planta: number;
  }
  