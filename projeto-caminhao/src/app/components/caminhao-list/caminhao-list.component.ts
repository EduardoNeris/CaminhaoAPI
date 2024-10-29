import { Component, OnInit } from '@angular/core';
import { CaminhaoService } from '../../caminhao.service';
import { Caminhao } from '../../caminhao';
import { CommonModule } from '@angular/common';
import { ModeloEnum } from '../../modelo';
import { PlantaEnum } from '../../planta';
import { MatDialog } from '@angular/material/dialog';
import { CaminhaoFormComponent } from '../caminhao-form/caminhao-form.component';

@Component({
  selector: 'app-caminhao-list',
  templateUrl: './caminhao-list.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class CaminhaoListComponent implements OnInit {
  caminhoes: Caminhao[] = [];

  constructor(
    private caminhaoService: CaminhaoService,
    public dialog: MatDialog
  ) {}

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

  openForm(caminhao?: Caminhao): void {
    const dialogRef = this.dialog.open(CaminhaoFormComponent, {
      width: '500px',
      data: caminhao,
    });

    dialogRef.afterClosed().subscribe((res) => {
      this.loadItems();
    });
  }
}
