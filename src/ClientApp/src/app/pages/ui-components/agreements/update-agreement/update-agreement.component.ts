
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { PartnerForUpdateDto } from 'src/app/_interface/dtos';
import { DataService } from 'src/app/shared/services/data.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-update-agreement',
  templateUrl: './update-agreement.component.html',
})
export class UpdateAgreementComponent implements OnInit {
  dataForm: FormGroup | any;
  partner: PartnerForUpdateDto | any;
  result: any;

  constructor(
    private repoService: RepositoryService,
    private toastr: ToastrService,
    private dataService: DataService,
    private dialogserve: DialogService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private Ref: MatDialogRef<UpdateAgreementComponent>
  ) {}

  ngOnInit() {
    this.dataForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(60),
      ]),
      contactEmail: new FormControl('', [
        Validators.required,
        Validators.maxLength(60),
      ]),
      contactPhone: new FormControl('', [
        Validators.required,
        Validators.maxLength(100),
      ]),
    });

    this.result = this.data;
    this.getPartnerToUpdate();
  }

  public validateControl = (controlName: string) => {
    return (
      this.dataForm?.get(controlName)?.invalid &&
      this.dataForm?.get(controlName)?.touched
    );
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.dataForm?.get(controlName)?.hasError(errorName);
  };

  public createData = (dataFormValue: any) => {
    if (this.dataForm.valid) {
      this.executeDataCreation(dataFormValue);
    }
  };

  private executeDataCreation = (dataFormValue: any) => {
    let data: PartnerForUpdateDto = {
      name: dataFormValue.name,
      contactEmail: dataFormValue.contactEmail,
      contactPhone: dataFormValue.contactPhone,
    };
    let id = this.result.id;
    const apiUri: string = `api/v0/partners/${id}`;
    this.repoService.update(apiUri, data).subscribe(
      (res) => {
        this.dialogserve.openSuccessDialog("The partner has been updated successfully.")
        .afterClosed()
        .subscribe((res) => {
          this.dataService.triggerRefreshTab1();
          this.Ref.close([]);
        });
      },
      (error) => {
        this.toastr.error(error);
      }
    );
  };

  private getPartnerToUpdate = () => {
    let id = this.result.id;
    const Uri: string = `api/v0/partners/${id}`;
    this.repoService.getData(Uri).subscribe({
      next: (own: PartnerForUpdateDto | any) => {
        this.partner = own.data[0];
        this.dataForm.patchValue(this.partner);
      },
      error: (err) => {
        this.toastr.success(err);
      },
    });
  };

  closeModal() {
    this.Ref.close([]);
  }
}
