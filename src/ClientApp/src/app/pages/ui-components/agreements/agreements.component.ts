import { HttpErrorResponse } from '@angular/common/http';
import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {PartnerAgreementDto, PartnerDto} from 'src/app/_interface/dtos';
import { AddAgreementComponent } from './add-agreement/add-agreement.component';
import { UpdateAgreementComponent } from './update-agreement/update-agreement.component';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-agreements',
  templateUrl: './agreements.component.html',
})
export class AgreementsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'name',
    'partnerName',
    'createdDate',
    'calculatedRisk',
  ];
  public dataSource = new MatTableDataSource<PartnerDto>();
  errorMessage: any;
  showError: boolean;
  private refreshSubscription!: Subscription;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private repoService: RepositoryService,
    private router: Router,
    private toastr: ToastrService,
    private dialog: MatDialog,
    private dataService: DataService,
    private dialogserve: DialogService
  ) {
    this.refreshSubscription = this.dataService.refreshTab1$.subscribe(() => {
      this.getAgreements();
    });
  }
  ngOnInit(): void {
    this.getAgreements();
  }

  public getAgreements() {
    this.repoService.getData('api/v0/agreements').subscribe(
      (res : any) => {
        this.dataSource.data = res.data as PartnerAgreementDto[];
      },
      (err) => {
        console.log(err);
      }
    );
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };

  addAgreement() {
    const popup = this.dialog.open(AddAgreementComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
    });
  }

  updateAgreement(id: string) {
    const popup = this.dialog.open(UpdateAgreementComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
      data: {
        id: id,
      },
    });
  }

  deleteAgreement(id: any) {
    this.dialogserve
      .openConfirmDialog('Are you sure, you want to delete the agreement?')
      .afterClosed()
      .subscribe((res) => {
        if (res) {
          const deleteUri: string = `api/v0/agreements/${id}`;
          this.repoService.delete(deleteUri).subscribe((res) => {
            this.dialogserve
              .openSuccessDialog('The agreement has been deleted successfully.')
              .afterClosed()
              .subscribe((res) => {
                this.getAgreements();
              });
          });
        }
      });
  }
}
