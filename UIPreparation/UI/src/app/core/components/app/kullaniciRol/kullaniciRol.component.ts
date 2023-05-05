import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { KullaniciRol } from './models/KullaniciRol';
import { KullaniciRolService } from './services/KullaniciRol.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kullaniciRol',
	templateUrl: './kullaniciRol.component.html',
	styleUrls: ['./kullaniciRol.component.scss']
})
export class KullaniciRolComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','kRMKLNKOD','rolId','birlikId','iDRBRMKOD','durum', 'update','delete'];

	kullaniciRolList:KullaniciRol[];
	kullaniciRol:KullaniciRol=new KullaniciRol();

	kullaniciRolAddForm: FormGroup;


	kullaniciRolId:number;

	constructor(private kullaniciRolService:KullaniciRolService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKullaniciRolList();
    }

	ngOnInit() {

		this.createKullaniciRolAddForm();
	}


	getKullaniciRolList() {
		this.kullaniciRolService.getKullaniciRolList().subscribe(data => {
			this.kullaniciRolList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kullaniciRolAddForm.valid) {
			this.kullaniciRol = Object.assign({}, this.kullaniciRolAddForm.value)

			if (this.kullaniciRol.id == 0)
				this.addKullaniciRol();
			else
				this.updateKullaniciRol();
		}

	}

	addKullaniciRol(){

		this.kullaniciRolService.addKullaniciRol(this.kullaniciRol).subscribe(data => {
			this.getKullaniciRolList();
			this.kullaniciRol = new KullaniciRol();
			jQuery('#kullanicirol').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciRolAddForm);

		})

	}

	updateKullaniciRol(){

		this.kullaniciRolService.updateKullaniciRol(this.kullaniciRol).subscribe(data => {

			var index=this.kullaniciRolList.findIndex(x=>x.id==this.kullaniciRol.id);
			this.kullaniciRolList[index]=this.kullaniciRol;
			this.dataSource = new MatTableDataSource(this.kullaniciRolList);
            this.configDataTable();
			this.kullaniciRol = new KullaniciRol();
			jQuery('#kullanicirol').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciRolAddForm);

		})

	}

	createKullaniciRolAddForm() {
		this.kullaniciRolAddForm = this.formBuilder.group({		
			id : [0],
kRMKLNKOD : [0, Validators.required],
rolId : [0, Validators.required],
birlikId : [0, Validators.required],
iDRBRMKOD : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteKullaniciRol(kullaniciRolId:number){
		this.kullaniciRolService.deleteKullaniciRol(kullaniciRolId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kullaniciRolList=this.kullaniciRolList.filter(x=> x.id!=kullaniciRolId);
			this.dataSource = new MatTableDataSource(this.kullaniciRolList);
			this.configDataTable();
		})
	}

	getKullaniciRolById(kullaniciRolId:number){
		this.clearFormGroup(this.kullaniciRolAddForm);
		this.kullaniciRolService.getKullaniciRolById(kullaniciRolId).subscribe(data=>{
			this.kullaniciRol=data;
			this.kullaniciRolAddForm.patchValue(data);
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
