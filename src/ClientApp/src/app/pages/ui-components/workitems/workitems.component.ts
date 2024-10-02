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
import { AddWorkItemComponent } from './add-workitem/add-workitem.component';
import { UpdateWorkItemComponent } from './update-workitem/update-workitem.component';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-workitems',
  templateUrl: './workitems.component.html',
})
export class WorkItemsComponent implements OnInit, AfterViewInit {
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
      this.getWorkItems();
    });
  }
  ngOnInit(): void {
    this.getWorkItems();
  }

  public getWorkItems() {
    this.repoService.getData('api/v0/workitems').subscribe(
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

  addWorkItem() {
    const popup = this.dialog.open(AddWorkItemComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
    });
  }

  updateWorkItem(id: string) {
    const popup = this.dialog.open(UpdateWorkItemComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
      data: {
        id: id,
      },
    });
  }

  deleteWorkItem(id: any) {
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
                this.getWorkItems();
              });
          });
        }
      });
  }
}
