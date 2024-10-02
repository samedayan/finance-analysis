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
import { PartnerDto } from 'src/app/_interface/dtos';
import { AddPartnerComponent } from './add-partner/add-partner.component';
import { UpdatePartnerComponent } from './update-partner/update-partner.component';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-partners',
  templateUrl: './partners.component.html',
})
export class PartnersComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'action',
    'name',
    'statusName',
    'createdDate',
    'contactEmail',
    'contactPhone',
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
      this.getPartners();
    });
  }
  ngOnInit(): void {
    this.getPartners();
  }

  public getPartners() {
    this.repoService.getData('api/v0/partners').subscribe(
      (res : any) => {
        this.dataSource.data = res.data as PartnerDto[];
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

  addPartner() {
    const popup = this.dialog.open(AddPartnerComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
    });
  }

  updatePartner(id: string) {
    const popup = this.dialog.open(UpdatePartnerComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
      data: {
        id: id,
      },
    });
  }

  deletePartner(id: any) {
    this.dialogserve
      .openConfirmDialog('Are you sure, you want to delete the partner?')
      .afterClosed()
      .subscribe((res) => {
        if (res) {
          const deleteUri: string = `api/v0/partners/${id}`;
          this.repoService.delete(deleteUri).subscribe((res) => {
            this.dialogserve
              .openSuccessDialog('The partner has been deleted successfully.')
              .afterClosed()
              .subscribe((res) => {
                this.getPartners();
              });
          });
        }
      });
  }

  public redirectToDetails = async (id: string) => {
    let url: string = `/ui-components/partner-detail/${id}`;
    this.router.navigate([url]);
  };
}
