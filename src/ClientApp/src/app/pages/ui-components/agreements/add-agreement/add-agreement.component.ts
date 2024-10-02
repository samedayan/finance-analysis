import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { PartnerForCreationDto } from 'src/app/_interface/dtos';
import { DataService } from 'src/app/shared/services/data.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-add-agreement',
  templateUrl: './add-agreement.component.html'
})
export class AddAgreementComponent implements OnInit {
  dataForm: FormGroup |any;

  constructor(
    private repoService: RepositoryService,
    private toastr: ToastrService,
    private dataService: DataService,
    private dialogserve: DialogService,
    private Ref: MatDialogRef<AddAgreementComponent>) {}

  ngOnInit() {
    this.dataForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      contactEmail: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      contactPhone: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    });
  }

  public validateControl = (controlName: string) => {
    return this.dataForm?.get(controlName)?.invalid && this.dataForm?.get(controlName)?.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.dataForm?.get(controlName)?.hasError(errorName)
  }

  public createData = (dataFormValue: any) => {
    if (this.dataForm.valid) {
      this.executeDataCreation(dataFormValue);
    }
  };

  private executeDataCreation = (dataFormValue: any) => {

    let data: PartnerForCreationDto = {
      name: dataFormValue.name,
      contactEmail: dataFormValue.contactEmail,
      contactPhone: dataFormValue.contactPhone,
    };

    const apiUri: string = `api/v0/partners`;
    this.repoService.create(apiUri, data).subscribe(
      (res) => {
        this.dialogserve.openSuccessDialog("The partner has been added successfully.")
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

  closeModal(){
    this.Ref.close([]);
  }
}
