import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { RolMenuIslevObje } from './models/RolMenuIslevObje';
import { RolMenuIslevObjeService } from './services/RolMenuIslevObje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-rolMenuIslevObje',
	templateUrl: './rolMenuIslevObje.component.html',
	styleUrls: ['./rolMenuIslevObje.component.scss']
})
export class RolMenuIslevObjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','rolId','menuId','islevId','objeId','durum', 'update','delete'];

	rolMenuIslevObjeList:RolMenuIslevObje[];
	rolMenuIslevObje:RolMenuIslevObje=new RolMenuIslevObje();

	rolMenuIslevObjeAddForm: FormGroup;


	rolMenuIslevObjeId:number;

	constructor(private rolMenuIslevObjeService:RolMenuIslevObjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getRolMenuIslevObjeList();
    }

	ngOnInit() {

		this.createRolMenuIslevObjeAddForm();
	}


	getRolMenuIslevObjeList() {
		this.rolMenuIslevObjeService.getRolMenuIslevObjeList().subscribe(data => {
			this.rolMenuIslevObjeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.rolMenuIslevObjeAddForm.valid) {
			this.rolMenuIslevObje = Object.assign({}, this.rolMenuIslevObjeAddForm.value)

			if (this.rolMenuIslevObje.id == 0)
				this.addRolMenuIslevObje();
			else
				this.updateRolMenuIslevObje();
		}

	}

	addRolMenuIslevObje(){

		this.rolMenuIslevObjeService.addRolMenuIslevObje(this.rolMenuIslevObje).subscribe(data => {
			this.getRolMenuIslevObjeList();
			this.rolMenuIslevObje = new RolMenuIslevObje();
			jQuery('#rolmenu?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rolMenuIslevObjeAddForm);

		})

	}

	updateRolMenuIslevObje(){

		this.rolMenuIslevObjeService.updateRolMenuIslevObje(this.rolMenuIslevObje).subscribe(data => {

			var index=this.rolMenuIslevObjeList.findIndex(x=>x.id==this.rolMenuIslevObje.id);
			this.rolMenuIslevObjeList[index]=this.rolMenuIslevObje;
			this.dataSource = new MatTableDataSource(this.rolMenuIslevObjeList);
            this.configDataTable();
			this.rolMenuIslevObje = new RolMenuIslevObje();
			jQuery('#rolmenu?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rolMenuIslevObjeAddForm);

		})

	}

	createRolMenuIslevObjeAddForm() {
		this.rolMenuIslevObjeAddForm = this.formBuilder.group({		
			id : [0],
rolId : [0, Validators.required],
menuId : [0, Validators.required],
islevId : [0, Validators.required],
objeId : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteRolMenuIslevObje(rolMenuIslevObjeId:number){
		this.rolMenuIslevObjeService.deleteRolMenuIslevObje(rolMenuIslevObjeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.rolMenuIslevObjeList=this.rolMenuIslevObjeList.filter(x=> x.id!=rolMenuIslevObjeId);
			this.dataSource = new MatTableDataSource(this.rolMenuIslevObjeList);
			this.configDataTable();
		})
	}

	getRolMenuIslevObjeById(rolMenuIslevObjeId:number){
		this.clearFormGroup(this.rolMenuIslevObjeAddForm);
		this.rolMenuIslevObjeService.getRolMenuIslevObjeById(rolMenuIslevObjeId).subscribe(data=>{
			this.rolMenuIslevObje=data;
			this.rolMenuIslevObjeAddForm.patchValue(data);
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
