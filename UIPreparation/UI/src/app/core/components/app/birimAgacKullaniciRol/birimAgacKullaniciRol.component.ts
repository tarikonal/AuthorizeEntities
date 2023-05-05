import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { BirimAgacKullaniciRol } from './models/BirimAgacKullaniciRol';
import { BirimAgacKullaniciRolService } from './services/BirimAgacKullaniciRol.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-birimAgacKullaniciRol',
	templateUrl: './birimAgacKullaniciRol.component.html',
	styleUrls: ['./birimAgacKullaniciRol.component.scss']
})
export class BirimAgacKullaniciRolComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','birimAgacId','kRMKLNKOD','rolId','sureliGorevlendirme','gorevBaslangicTarihi','gorevBitisTarihi','durum', 'update','delete'];

	birimAgacKullaniciRolList:BirimAgacKullaniciRol[];
	birimAgacKullaniciRol:BirimAgacKullaniciRol=new BirimAgacKullaniciRol();

	birimAgacKullaniciRolAddForm: FormGroup;


	birimAgacKullaniciRolId:number;

	constructor(private birimAgacKullaniciRolService:BirimAgacKullaniciRolService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getBirimAgacKullaniciRolList();
    }

	ngOnInit() {

		this.createBirimAgacKullaniciRolAddForm();
	}


	getBirimAgacKullaniciRolList() {
		this.birimAgacKullaniciRolService.getBirimAgacKullaniciRolList().subscribe(data => {
			this.birimAgacKullaniciRolList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.birimAgacKullaniciRolAddForm.valid) {
			this.birimAgacKullaniciRol = Object.assign({}, this.birimAgacKullaniciRolAddForm.value)

			if (this.birimAgacKullaniciRol.id == 0)
				this.addBirimAgacKullaniciRol();
			else
				this.updateBirimAgacKullaniciRol();
		}

	}

	addBirimAgacKullaniciRol(){

		this.birimAgacKullaniciRolService.addBirimAgacKullaniciRol(this.birimAgacKullaniciRol).subscribe(data => {
			this.getBirimAgacKullaniciRolList();
			this.birimAgacKullaniciRol = new BirimAgacKullaniciRol();
			jQuery('#birimagackullanicirol').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimAgacKullaniciRolAddForm);

		})

	}

	updateBirimAgacKullaniciRol(){

		this.birimAgacKullaniciRolService.updateBirimAgacKullaniciRol(this.birimAgacKullaniciRol).subscribe(data => {

			var index=this.birimAgacKullaniciRolList.findIndex(x=>x.id==this.birimAgacKullaniciRol.id);
			this.birimAgacKullaniciRolList[index]=this.birimAgacKullaniciRol;
			this.dataSource = new MatTableDataSource(this.birimAgacKullaniciRolList);
            this.configDataTable();
			this.birimAgacKullaniciRol = new BirimAgacKullaniciRol();
			jQuery('#birimagackullanicirol').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimAgacKullaniciRolAddForm);

		})

	}

	createBirimAgacKullaniciRolAddForm() {
		this.birimAgacKullaniciRolAddForm = this.formBuilder.group({		
			id : [0],
birimAgacId : [0, Validators.required],
kRMKLNKOD : [0, Validators.required],
rolId : [0, Validators.required],
sureliGorevlendirme : [false, Validators.required],
gorevBaslangicTarihi : [null, Validators.required],
gorevBitisTarihi : [null, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteBirimAgacKullaniciRol(birimAgacKullaniciRolId:number){
		this.birimAgacKullaniciRolService.deleteBirimAgacKullaniciRol(birimAgacKullaniciRolId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.birimAgacKullaniciRolList=this.birimAgacKullaniciRolList.filter(x=> x.id!=birimAgacKullaniciRolId);
			this.dataSource = new MatTableDataSource(this.birimAgacKullaniciRolList);
			this.configDataTable();
		})
	}

	getBirimAgacKullaniciRolById(birimAgacKullaniciRolId:number){
		this.clearFormGroup(this.birimAgacKullaniciRolAddForm);
		this.birimAgacKullaniciRolService.getBirimAgacKullaniciRolById(birimAgacKullaniciRolId).subscribe(data=>{
			this.birimAgacKullaniciRol=data;
			this.birimAgacKullaniciRolAddForm.patchValue(data);
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
