import { Component, Inject, OnInit } from '@angular/core';
import { CaminhaoService } from '../../caminhao.service';
import { Caminhao } from '../../caminhao';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ModeloEnum } from '../../modelo';
import { PlantaEnum } from '../../planta';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogModule,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-caminhao-form',
  templateUrl: './caminhao-form.component.html',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
  ],
})
export class CaminhaoFormComponent implements OnInit {
  public caminhaoForm: FormGroup;

  constructor(
    private caminhaoService: CaminhaoService,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) private caminhao: Caminhao,
    public dialog: MatDialog
  ) {
    this.caminhaoForm = this.fb.group({
      id: [caminhao ? caminhao.id : 0, Validators.required],
      modelo: [caminhao ? caminhao.modelo : '', Validators.required],
      anoFabricacao: [
        caminhao ? caminhao.anoFabricacao : '',
        [Validators.required, Validators.min(1900), Validators.max(2100)],
      ],
      codigoChassi: [
        caminhao ? caminhao.codigoChassi : '',
        [Validators.required, Validators.maxLength(17)],
      ],
      cor: [
        caminhao ? caminhao.cor : '',
        [Validators.required, Validators.maxLength(30)],
      ],
      planta: [caminhao ? caminhao.planta : '', Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.caminhaoForm.valid) {
      const caminhao: Caminhao = this.caminhaoForm.value;

      if (caminhao.id === 0) {
        this.caminhaoService.createCaminhao(caminhao).subscribe(() => {
          this.dialog.closeAll();
        });
      } else {
        this.caminhaoService.updateCaminhao(caminhao).subscribe(() => {
          this.dialog.closeAll();
        });
      }
    }
  }
}
