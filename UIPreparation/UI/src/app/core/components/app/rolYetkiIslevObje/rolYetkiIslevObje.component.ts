import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { RolYetkiIslevObje } from './models/RolYetkiIslevObje';
import { RolYetkiIslevObjeService } from './services/RolYetkiIslevObje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-rolYetkiIslevObje',
	templateUrl: './rolYetkiIslevObje.component.html',
	styleUrls: ['./rolYetkiIslevObje.component.scss']
})
export class RolYetkiIslevObjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','rolId','yetkiId','islevId','objeId','durum', 'update','delete'];

	rolYetkiIslevObjeList:RolYetkiIslevObje[];
	rolYetkiIslevObje:RolYetkiIslevObje=new RolYetkiIslevObje();

	rolYetkiIslevObjeAddForm: FormGroup;


	rolYetkiIslevObjeId:number;

	constructor(private rolYetkiIslevObjeService:RolYetkiIslevObjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getRolYetkiIslevObjeList();
    }

	ngOnInit() {

		this.createRolYetkiIslevObjeAddForm();
	}


	getRolYetkiIslevObjeList() {
		this.rolYetkiIslevObjeService.getRolYetkiIslevObjeList().subscribe(data => {
			this.rolYetkiIslevObjeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.rolYetkiIslevObjeAddForm.valid) {
			this.rolYetkiIslevObje = Object.assign({}, this.rolYetkiIslevObjeAddForm.value)

			if (this.rolYetkiIslevObje.id == 0)
				this.addRolYetkiIslevObje();
			else
				this.updateRolYetkiIslevObje();
		}

	}

	addRolYetkiIslevObje(){

		this.rolYetkiIslevObjeService.addRolYetkiIslevObje(this.rolYetkiIslevObje).subscribe(data => {
			this.getRolYetkiIslevObjeList();
			this.rolYetkiIslevObje = new RolYetkiIslevObje();
			jQuery('#rolyetki?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rolYetkiIslevObjeAddForm);

		})

	}

	updateRolYetkiIslevObje(){

		this.rolYetkiIslevObjeService.updateRolYetkiIslevObje(this.rolYetkiIslevObje).subscribe(data => {

			var index=this.rolYetkiIslevObjeList.findIndex(x=>x.id==this.rolYetkiIslevObje.id);
			this.rolYetkiIslevObjeList[index]=this.rolYetkiIslevObje;
			this.dataSource = new MatTableDataSource(this.rolYetkiIslevObjeList);
            this.configDataTable();
			this.rolYetkiIslevObje = new RolYetkiIslevObje();
			jQuery('#rolyetki?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rolYetkiIslevObjeAddForm);

		})

	}

	createRolYetkiIslevObjeAddForm() {
		this.rolYetkiIslevObjeAddForm = this.formBuilder.group({		
			id : [0],
rolId : [0, Validators.required],
yetkiId : [0, Validators.required],
islevId : [0, Validators.required],
objeId : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteRolYetkiIslevObje(rolYetkiIslevObjeId:number){
		this.rolYetkiIslevObjeService.deleteRolYetkiIslevObje(rolYetkiIslevObjeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.rolYetkiIslevObjeList=this.rolYetkiIslevObjeList.filter(x=> x.id!=rolYetkiIslevObjeId);
			this.dataSource = new MatTableDataSource(this.rolYetkiIslevObjeList);
			this.configDataTable();
		})
	}

	getRolYetkiIslevObjeById(rolYetkiIslevObjeId:number){
		this.clearFormGroup(this.rolYetkiIslevObjeAddForm);
		this.rolYetkiIslevObjeService.getRolYetkiIslevObjeById(rolYetkiIslevObjeId).subscribe(data=>{
			this.rolYetkiIslevObje=data;
			this.rolYetkiIslevObjeAddForm.patchValue(data);
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
