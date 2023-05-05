import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { KullaniciYetkiIslevEngel } from './models/KullaniciYetkiIslevEngel';
import { KullaniciYetkiIslevEngelService } from './services/KullaniciYetkiIslevEngel.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kullaniciYetkiIslevEngel',
	templateUrl: './kullaniciYetkiIslevEngel.component.html',
	styleUrls: ['./kullaniciYetkiIslevEngel.component.scss']
})
export class KullaniciYetkiIslevEngelComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','yetkiId','islevId','kRMKLNKOD','durum', 'update','delete'];

	kullaniciYetkiIslevEngelList:KullaniciYetkiIslevEngel[];
	kullaniciYetkiIslevEngel:KullaniciYetkiIslevEngel=new KullaniciYetkiIslevEngel();

	kullaniciYetkiIslevEngelAddForm: FormGroup;


	kullaniciYetkiIslevEngelId:number;

	constructor(private kullaniciYetkiIslevEngelService:KullaniciYetkiIslevEngelService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKullaniciYetkiIslevEngelList();
    }

	ngOnInit() {

		this.createKullaniciYetkiIslevEngelAddForm();
	}


	getKullaniciYetkiIslevEngelList() {
		this.kullaniciYetkiIslevEngelService.getKullaniciYetkiIslevEngelList().subscribe(data => {
			this.kullaniciYetkiIslevEngelList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kullaniciYetkiIslevEngelAddForm.valid) {
			this.kullaniciYetkiIslevEngel = Object.assign({}, this.kullaniciYetkiIslevEngelAddForm.value)

			if (this.kullaniciYetkiIslevEngel.id == 0)
				this.addKullaniciYetkiIslevEngel();
			else
				this.updateKullaniciYetkiIslevEngel();
		}

	}

	addKullaniciYetkiIslevEngel(){

		this.kullaniciYetkiIslevEngelService.addKullaniciYetkiIslevEngel(this.kullaniciYetkiIslevEngel).subscribe(data => {
			this.getKullaniciYetkiIslevEngelList();
			this.kullaniciYetkiIslevEngel = new KullaniciYetkiIslevEngel();
			jQuery('#kullaniciyetki?slevengel').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciYetkiIslevEngelAddForm);

		})

	}

	updateKullaniciYetkiIslevEngel(){

		this.kullaniciYetkiIslevEngelService.updateKullaniciYetkiIslevEngel(this.kullaniciYetkiIslevEngel).subscribe(data => {

			var index=this.kullaniciYetkiIslevEngelList.findIndex(x=>x.id==this.kullaniciYetkiIslevEngel.id);
			this.kullaniciYetkiIslevEngelList[index]=this.kullaniciYetkiIslevEngel;
			this.dataSource = new MatTableDataSource(this.kullaniciYetkiIslevEngelList);
            this.configDataTable();
			this.kullaniciYetkiIslevEngel = new KullaniciYetkiIslevEngel();
			jQuery('#kullaniciyetki?slevengel').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciYetkiIslevEngelAddForm);

		})

	}

	createKullaniciYetkiIslevEngelAddForm() {
		this.kullaniciYetkiIslevEngelAddForm = this.formBuilder.group({		
			id : [0],
yetkiId : [0, Validators.required],
islevId : [0, Validators.required],
kRMKLNKOD : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteKullaniciYetkiIslevEngel(kullaniciYetkiIslevEngelId:number){
		this.kullaniciYetkiIslevEngelService.deleteKullaniciYetkiIslevEngel(kullaniciYetkiIslevEngelId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kullaniciYetkiIslevEngelList=this.kullaniciYetkiIslevEngelList.filter(x=> x.id!=kullaniciYetkiIslevEngelId);
			this.dataSource = new MatTableDataSource(this.kullaniciYetkiIslevEngelList);
			this.configDataTable();
		})
	}

	getKullaniciYetkiIslevEngelById(kullaniciYetkiIslevEngelId:number){
		this.clearFormGroup(this.kullaniciYetkiIslevEngelAddForm);
		this.kullaniciYetkiIslevEngelService.getKullaniciYetkiIslevEngelById(kullaniciYetkiIslevEngelId).subscribe(data=>{
			this.kullaniciYetkiIslevEngel=data;
			this.kullaniciYetkiIslevEngelAddForm.patchValue(data);
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
