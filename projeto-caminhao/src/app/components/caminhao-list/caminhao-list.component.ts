import { Component, OnInit } from '@angular/core';
import { CaminhaoService } from '../../caminhao.service';
import { Caminhao } from '../../caminhao';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-caminhao-list',
  templateUrl: './caminhao-list.component.html',
  styleUrls: ['./caminhao-list.component.css'],
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    FormsModule
  ]
})
export class CaminhaoListComponent implements OnInit {
  caminhoes: Caminhao[] = [];
  novoCaminhao: Caminhao = {
    Id: 0, 
    Modelo: 0, 
    AnoFabricacao: 0,
    CodigoChassi: '',
    Cor: '',
    Planta: 0
  };
  isEditing: boolean = false;
  editingCaminhaoId: number | null = null;

  constructor(private caminhaoService: CaminhaoService) {}

  ngOnInit(): void {
    this.caminhaoService.getCaminhoes().subscribe((caminhoes) => {
      this.caminhoes = caminhoes;
    });
  }

  addCaminhao() {
    if (this.novoCaminhao.Modelo && this.novoCaminhao.AnoFabricacao) {
      this.novoCaminhao.Id = this.caminhoes.length ? Math.max(...this.caminhoes.map(c => c.Id)) + 1 : 1;
      this.caminhaoService.createCaminhao(this.novoCaminhao).subscribe(() => {
        this.novoCaminhao = {
          Id: 0, 
          Modelo: 0, 
          AnoFabricacao: 0,
          CodigoChassi: '',
          Cor: '',
          Planta: 0
        };
      });
    }
  }

  editCaminhao(caminhao: Caminhao) {
    this.isEditing = true;
    this.editingCaminhaoId = caminhao.Id;
    this.novoCaminhao = { ...caminhao };
  }

  updateCaminhao() {
    if (this.novoCaminhao.Modelo && this.novoCaminhao.AnoFabricacao && this.editingCaminhaoId) {
      this.caminhaoService.updateCaminhao(this.novoCaminhao).subscribe(() => {
        this.cancelEdit();
      });
    }
  }

  deleteCaminhao(id: number) {
    this.caminhaoService.deleteCaminhao(id).subscribe();
  }

  cancelEdit() {
    this.isEditing = false;
    this.editingCaminhaoId = null;
    this.novoCaminhao = {
      Id: 0, 
      Modelo: 0, 
      AnoFabricacao: 0,
      CodigoChassi: '',
      Cor: '',
      Planta: 0
    };
  }
}
