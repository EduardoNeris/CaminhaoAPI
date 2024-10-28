import { Component, OnInit } from '@angular/core';
import { CaminhaoService } from '../../caminhao.service';
import { Caminhao } from '../../caminhao';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { ModeloEnum } from '../../modelo';
import { PlantaEnum } from '../../planta';

@Component({
  selector: 'app-caminhao-list',
  templateUrl: './caminhao-list.component.html',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, MatButtonModule],
})
export class CaminhaoListComponent implements OnInit {
  caminhoes: Caminhao[] = [];

  constructor(private caminhaoService: CaminhaoService) {}

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    this.caminhaoService.getCaminhoes().subscribe((caminhoes) => {
      this.caminhoes = caminhoes;
    });
  }

  delete(id: number) {
    this.caminhaoService.deleteCaminhao(id).subscribe(() => {
      this.loadItems();
    });
  }

  getModeloDescription(modelo: ModeloEnum) {
    return ModeloEnum[modelo];
  }

  getPlantaDescription(planta: PlantaEnum) {
    return PlantaEnum[planta];
  }
}
