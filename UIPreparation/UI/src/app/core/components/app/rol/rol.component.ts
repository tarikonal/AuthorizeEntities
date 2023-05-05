import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Rol } from './models/Rol';
import { RolService } from './services/Rol.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-rol',
	templateUrl: './rol.component.html',
	styleUrls: ['./rol.component.scss']
})
export class RolComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','projeId','rolTipiId','rolSeviyeId','keyValue','rolAdi','aciklama','varsayilanMi','durum', 'update','delete'];

	rolList:Rol[];
	rol:Rol=new Rol();

	rolAddForm: FormGroup;


	rolId:number;

	constructor(private rolService:RolService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getRolList();
    }

	ngOnInit() {

		this.createRolAddForm();
	}


	getRolList() {
		this.rolService.getRolList().subscribe(data => {
			this.rolList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.rolAddForm.valid) {
			this.rol = Object.assign({}, this.rolAddForm.value)

			if (this.rol.id == 0)
				this.addRol();
			else
				this.updateRol();
		}

	}

	addRol(){

		this.rolService.addRol(this.rol).subscribe(data => {
			this.getRolList();
			this.rol = new Rol();
			jQuery('#rol').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rolAddForm);

		})

	}

	updateRol(){

		this.rolService.updateRol(this.rol).subscribe(data => {

			var index=this.rolList.findIndex(x=>x.id==this.rol.id);
			this.rolList[index]=this.rol;
			this.dataSource = new MatTableDataSource(this.rolList);
            this.configDataTable();
			this.rol = new Rol();
			jQuery('#rol').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rolAddForm);

		})

	}

	createRolAddForm() {
		this.rolAddForm = this.formBuilder.group({		
			id : [0],
projeId : [0, Validators.required],
rolTipiId : [0, Validators.required],
rolSeviyeId : [0, Validators.required],
keyValue : ["", Validators.required],
rolAdi : ["", Validators.required],
aciklama : ["", Validators.required],
varsayilanMi : [false, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteRol(rolId:number){
		this.rolService.deleteRol(rolId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.rolList=this.rolList.filter(x=> x.id!=rolId);
			this.dataSource = new MatTableDataSource(this.rolList);
			this.configDataTable();
		})
	}

	getRolById(rolId:number){
		this.clearFormGroup(this.rolAddForm);
		this.rolService.getRolById(rolId).subscribe(data=>{
			this.rol=data;
			this.rolAddForm.patchValue(data);
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
