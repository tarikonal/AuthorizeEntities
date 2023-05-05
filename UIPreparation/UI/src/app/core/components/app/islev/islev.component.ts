import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Islev } from './models/Islev';
import { IslevService } from './services/Islev.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-islev',
	templateUrl: './islev.component.html',
	styleUrls: ['./islev.component.scss']
})
export class IslevComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','islevAdi','aciklama','durum','projeId', 'update','delete'];

	islevList:Islev[];
	islev:Islev=new Islev();

	islevAddForm: FormGroup;


	islevId:number;

	constructor(private islevService:IslevService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getIslevList();
    }

	ngOnInit() {

		this.createIslevAddForm();
	}


	getIslevList() {
		this.islevService.getIslevList().subscribe(data => {
			this.islevList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.islevAddForm.valid) {
			this.islev = Object.assign({}, this.islevAddForm.value)

			if (this.islev.id == 0)
				this.addIslev();
			else
				this.updateIslev();
		}

	}

	addIslev(){

		this.islevService.addIslev(this.islev).subscribe(data => {
			this.getIslevList();
			this.islev = new Islev();
			jQuery('#islev').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.islevAddForm);

		})

	}

	updateIslev(){

		this.islevService.updateIslev(this.islev).subscribe(data => {

			var index=this.islevList.findIndex(x=>x.id==this.islev.id);
			this.islevList[index]=this.islev;
			this.dataSource = new MatTableDataSource(this.islevList);
            this.configDataTable();
			this.islev = new Islev();
			jQuery('#islev').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.islevAddForm);

		})

	}

	createIslevAddForm() {
		this.islevAddForm = this.formBuilder.group({		
			id : [0],
islevAdi : ["", Validators.required],
aciklama : ["", Validators.required],
durum : [false, Validators.required],
projeId : [0, Validators.required]
		})
	}

	deleteIslev(islevId:number){
		this.islevService.deleteIslev(islevId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.islevList=this.islevList.filter(x=> x.id!=islevId);
			this.dataSource = new MatTableDataSource(this.islevList);
			this.configDataTable();
		})
	}

	getIslevById(islevId:number){
		this.clearFormGroup(this.islevAddForm);
		this.islevService.getIslevById(islevId).subscribe(data=>{
			this.islev=data;
			this.islevAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
