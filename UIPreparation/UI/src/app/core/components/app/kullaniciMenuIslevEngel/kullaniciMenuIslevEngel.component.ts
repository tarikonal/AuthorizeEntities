import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { KullaniciMenuIslevEngel } from './models/KullaniciMenuIslevEngel';
import { KullaniciMenuIslevEngelService } from './services/KullaniciMenuIslevEngel.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kullaniciMenuIslevEngel',
	templateUrl: './kullaniciMenuIslevEngel.component.html',
	styleUrls: ['./kullaniciMenuIslevEngel.component.scss']
})
export class KullaniciMenuIslevEngelComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','menuId','islevId','kRMKLNKOD','durum', 'update','delete'];

	kullaniciMenuIslevEngelList:KullaniciMenuIslevEngel[];
	kullaniciMenuIslevEngel:KullaniciMenuIslevEngel=new KullaniciMenuIslevEngel();

	kullaniciMenuIslevEngelAddForm: FormGroup;


	kullaniciMenuIslevEngelId:number;

	constructor(private kullaniciMenuIslevEngelService:KullaniciMenuIslevEngelService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKullaniciMenuIslevEngelList();
    }

	ngOnInit() {

		this.createKullaniciMenuIslevEngelAddForm();
	}


	getKullaniciMenuIslevEngelList() {
		this.kullaniciMenuIslevEngelService.getKullaniciMenuIslevEngelList().subscribe(data => {
			this.kullaniciMenuIslevEngelList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kullaniciMenuIslevEngelAddForm.valid) {
			this.kullaniciMenuIslevEngel = Object.assign({}, this.kullaniciMenuIslevEngelAddForm.value)

			if (this.kullaniciMenuIslevEngel.id == 0)
				this.addKullaniciMenuIslevEngel();
			else
				this.updateKullaniciMenuIslevEngel();
		}

	}

	addKullaniciMenuIslevEngel(){

		this.kullaniciMenuIslevEngelService.addKullaniciMenuIslevEngel(this.kullaniciMenuIslevEngel).subscribe(data => {
			this.getKullaniciMenuIslevEngelList();
			this.kullaniciMenuIslevEngel = new KullaniciMenuIslevEngel();
			jQuery('#kullanicimenu?slevengel').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciMenuIslevEngelAddForm);

		})

	}

	updateKullaniciMenuIslevEngel(){

		this.kullaniciMenuIslevEngelService.updateKullaniciMenuIslevEngel(this.kullaniciMenuIslevEngel).subscribe(data => {

			var index=this.kullaniciMenuIslevEngelList.findIndex(x=>x.id==this.kullaniciMenuIslevEngel.id);
			this.kullaniciMenuIslevEngelList[index]=this.kullaniciMenuIslevEngel;
			this.dataSource = new MatTableDataSource(this.kullaniciMenuIslevEngelList);
            this.configDataTable();
			this.kullaniciMenuIslevEngel = new KullaniciMenuIslevEngel();
			jQuery('#kullanicimenu?slevengel').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kullaniciMenuIslevEngelAddForm);

		})

	}

	createKullaniciMenuIslevEngelAddForm() {
		this.kullaniciMenuIslevEngelAddForm = this.formBuilder.group({		
			id : [0],
menuId : [0, Validators.required],
islevId : [0, Validators.required],
kRMKLNKOD : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteKullaniciMenuIslevEngel(kullaniciMenuIslevEngelId:number){
		this.kullaniciMenuIslevEngelService.deleteKullaniciMenuIslevEngel(kullaniciMenuIslevEngelId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kullaniciMenuIslevEngelList=this.kullaniciMenuIslevEngelList.filter(x=> x.id!=kullaniciMenuIslevEngelId);
			this.dataSource = new MatTableDataSource(this.kullaniciMenuIslevEngelList);
			this.configDataTable();
		})
	}

	getKullaniciMenuIslevEngelById(kullaniciMenuIslevEngelId:number){
		this.clearFormGroup(this.kullaniciMenuIslevEngelAddForm);
		this.kullaniciMenuIslevEngelService.getKullaniciMenuIslevEngelById(kullaniciMenuIslevEngelId).subscribe(data=>{
			this.kullaniciMenuIslevEngel=data;
			this.kullaniciMenuIslevEngelAddForm.patchValue(data);
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
