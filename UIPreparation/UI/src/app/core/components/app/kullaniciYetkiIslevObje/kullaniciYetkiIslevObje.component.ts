import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { KullaniciYetkiIslevObje } from './models/KullaniciYetkiIslevObje';
import { KullaniciYetkiIslevObjeService } from './services/KullaniciYetkiIslevObje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kullaniciYetkiIslevObje',
	templateUrl: './kullaniciYetkiIslevObje.component.html',
	styleUrls: ['./kullaniciYetkiIslevObje.component.scss']
})
export class KullaniciYetkiIslevObjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','kRMKLNKOD','yetkiId','islevId','objeId','durum', 'update','delete'];

	kullaniciYetkiIslevObjeList:KullaniciYetkiIslevObje[];
	kullaniciYetkiIslevObje:KullaniciYetkiIslevObje=new KullaniciYetkiIslevObje();

	kullaniciYetkiIslevObjeAddForm: FormGroup;


	kullaniciYetkiIslevObjeId:number;

	constructor(private kullaniciYetkiIslevObjeService:KullaniciYetkiIslevObjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKullaniciYetkiIslevObjeList();
    }

	ngOnInit() {

		this.createKullaniciYetkiIslevObjeAddForm();
	}


	getKullaniciYetkiIslevObjeList() {
		this.kullaniciYetkiIslevObjeService.getKullaniciYetkiIslevObjeList().subscribe(data => {
			this.kullaniciYetkiIslevObjeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kullaniciYetkiIslevObjeAddForm.valid) {
			this.kullaniciYetkiIslevObje = Object.assign({}, this.kullaniciYetkiIslevObjeAddForm.value)

			if (this.kullaniciYetkiIslevObje.id == 0)
				this.addKullaniciYetkiIslevObje();
			else
				this.updateKullaniciYetkiIslevObje();
		}

	}

	addKullaniciYetkiIslevObje(){

		this.kullaniciYetkiIslevObjeService.addKullaniciYetkiIslevObje(this.kullaniciYetkiIslevObje).subscribe(data => {
			this.getKullaniciYetkiIslevObjeList();
			this.kullaniciYetkiIslevObje = new KullaniciYetkiIslevObje();
			jQuery('#kullaniciyetki?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciYetkiIslevObjeAddForm);

		})

	}

	updateKullaniciYetkiIslevObje(){

		this.kullaniciYetkiIslevObjeService.updateKullaniciYetkiIslevObje(this.kullaniciYetkiIslevObje).subscribe(data => {

			var index=this.kullaniciYetkiIslevObjeList.findIndex(x=>x.id==this.kullaniciYetkiIslevObje.id);
			this.kullaniciYetkiIslevObjeList[index]=this.kullaniciYetkiIslevObje;
			this.dataSource = new MatTableDataSource(this.kullaniciYetkiIslevObjeList);
            this.configDataTable();
			this.kullaniciYetkiIslevObje = new KullaniciYetkiIslevObje();
			jQuery('#kullaniciyetki?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciYetkiIslevObjeAddForm);

		})

	}

	createKullaniciYetkiIslevObjeAddForm() {
		this.kullaniciYetkiIslevObjeAddForm = this.formBuilder.group({		
			id : [0],
kRMKLNKOD : [0, Validators.required],
yetkiId : [0, Validators.required],
islevId : [0, Validators.required],
objeId : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteKullaniciYetkiIslevObje(kullaniciYetkiIslevObjeId:number){
		this.kullaniciYetkiIslevObjeService.deleteKullaniciYetkiIslevObje(kullaniciYetkiIslevObjeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kullaniciYetkiIslevObjeList=this.kullaniciYetkiIslevObjeList.filter(x=> x.id!=kullaniciYetkiIslevObjeId);
			this.dataSource = new MatTableDataSource(this.kullaniciYetkiIslevObjeList);
			this.configDataTable();
		})
	}

	getKullaniciYetkiIslevObjeById(kullaniciYetkiIslevObjeId:number){
		this.clearFormGroup(this.kullaniciYetkiIslevObjeAddForm);
		this.kullaniciYetkiIslevObjeService.getKullaniciYetkiIslevObjeById(kullaniciYetkiIslevObjeId).subscribe(data=>{
			this.kullaniciYetkiIslevObje=data;
			this.kullaniciYetkiIslevObjeAddForm.patchValue(data);
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
