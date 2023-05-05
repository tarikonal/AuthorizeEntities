import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { KullaniciMenuIslevObje } from './models/KullaniciMenuIslevObje';
import { KullaniciMenuIslevObjeService } from './services/KullaniciMenuIslevObje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kullaniciMenuIslevObje',
	templateUrl: './kullaniciMenuIslevObje.component.html',
	styleUrls: ['./kullaniciMenuIslevObje.component.scss']
})
export class KullaniciMenuIslevObjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','kRMKLNKOD','menuId','islevId','objeId','durum', 'update','delete'];

	kullaniciMenuIslevObjeList:KullaniciMenuIslevObje[];
	kullaniciMenuIslevObje:KullaniciMenuIslevObje=new KullaniciMenuIslevObje();

	kullaniciMenuIslevObjeAddForm: FormGroup;


	kullaniciMenuIslevObjeId:number;

	constructor(private kullaniciMenuIslevObjeService:KullaniciMenuIslevObjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKullaniciMenuIslevObjeList();
    }

	ngOnInit() {

		this.createKullaniciMenuIslevObjeAddForm();
	}


	getKullaniciMenuIslevObjeList() {
		this.kullaniciMenuIslevObjeService.getKullaniciMenuIslevObjeList().subscribe(data => {
			this.kullaniciMenuIslevObjeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kullaniciMenuIslevObjeAddForm.valid) {
			this.kullaniciMenuIslevObje = Object.assign({}, this.kullaniciMenuIslevObjeAddForm.value)

			if (this.kullaniciMenuIslevObje.id == 0)
				this.addKullaniciMenuIslevObje();
			else
				this.updateKullaniciMenuIslevObje();
		}

	}

	addKullaniciMenuIslevObje(){

		this.kullaniciMenuIslevObjeService.addKullaniciMenuIslevObje(this.kullaniciMenuIslevObje).subscribe(data => {
			this.getKullaniciMenuIslevObjeList();
			this.kullaniciMenuIslevObje = new KullaniciMenuIslevObje();
			jQuery('#kullanicimenu?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciMenuIslevObjeAddForm);

		})

	}

	updateKullaniciMenuIslevObje(){

		this.kullaniciMenuIslevObjeService.updateKullaniciMenuIslevObje(this.kullaniciMenuIslevObje).subscribe(data => {

			var index=this.kullaniciMenuIslevObjeList.findIndex(x=>x.id==this.kullaniciMenuIslevObje.id);
			this.kullaniciMenuIslevObjeList[index]=this.kullaniciMenuIslevObje;
			this.dataSource = new MatTableDataSource(this.kullaniciMenuIslevObjeList);
            this.configDataTable();
			this.kullaniciMenuIslevObje = new KullaniciMenuIslevObje();
			jQuery('#kullanicimenu?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciMenuIslevObjeAddForm);

		})

	}

	createKullaniciMenuIslevObjeAddForm() {
		this.kullaniciMenuIslevObjeAddForm = this.formBuilder.group({		
			id : [0],
kRMKLNKOD : [0, Validators.required],
menuId : [0, Validators.required],
islevId : [0, Validators.required],
objeId : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteKullaniciMenuIslevObje(kullaniciMenuIslevObjeId:number){
		this.kullaniciMenuIslevObjeService.deleteKullaniciMenuIslevObje(kullaniciMenuIslevObjeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kullaniciMenuIslevObjeList=this.kullaniciMenuIslevObjeList.filter(x=> x.id!=kullaniciMenuIslevObjeId);
			this.dataSource = new MatTableDataSource(this.kullaniciMenuIslevObjeList);
			this.configDataTable();
		})
	}

	getKullaniciMenuIslevObjeById(kullaniciMenuIslevObjeId:number){
		this.clearFormGroup(this.kullaniciMenuIslevObjeAddForm);
		this.kullaniciMenuIslevObjeService.getKullaniciMenuIslevObjeById(kullaniciMenuIslevObjeId).subscribe(data=>{
			this.kullaniciMenuIslevObje=data;
			this.kullaniciMenuIslevObjeAddForm.patchValue(data);
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
