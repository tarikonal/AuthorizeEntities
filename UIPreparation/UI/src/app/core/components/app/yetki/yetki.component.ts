import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Yetki } from './models/Yetki';
import { YetkiService } from './services/Yetki.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-yetki',
	templateUrl: './yetki.component.html',
	styleUrls: ['./yetki.component.scss']
})
export class YetkiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','yetkiAdi','aciklama','durum','projeId', 'update','delete'];

	yetkiList:Yetki[];
	yetki:Yetki=new Yetki();

	yetkiAddForm: FormGroup;


	yetkiId:number;

	constructor(private yetkiService:YetkiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getYetkiList();
    }

	ngOnInit() {

		this.createYetkiAddForm();
	}


	getYetkiList() {
		this.yetkiService.getYetkiList().subscribe(data => {
			this.yetkiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.yetkiAddForm.valid) {
			this.yetki = Object.assign({}, this.yetkiAddForm.value)

			if (this.yetki.id == 0)
				this.addYetki();
			else
				this.updateYetki();
		}

	}

	addYetki(){

		this.yetkiService.addYetki(this.yetki).subscribe(data => {
			this.getYetkiList();
			this.yetki = new Yetki();
			jQuery('#yetki').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.yetkiAddForm);

		})

	}

	updateYetki(){

		this.yetkiService.updateYetki(this.yetki).subscribe(data => {

			var index=this.yetkiList.findIndex(x=>x.id==this.yetki.id);
			this.yetkiList[index]=this.yetki;
			this.dataSource = new MatTableDataSource(this.yetkiList);
            this.configDataTable();
			this.yetki = new Yetki();
			jQuery('#yetki').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.yetkiAddForm);

		})

	}

	createYetkiAddForm() {
		this.yetkiAddForm = this.formBuilder.group({		
			id : [0],
yetkiAdi : ["", Validators.required],
aciklama : ["", Validators.required],
durum : [false, Validators.required],
projeId : [0, Validators.required]
		})
	}

	deleteYetki(yetkiId:number){
		this.yetkiService.deleteYetki(yetkiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.yetkiList=this.yetkiList.filter(x=> x.id!=yetkiId);
			this.dataSource = new MatTableDataSource(this.yetkiList);
			this.configDataTable();
		})
	}

	getYetkiById(yetkiId:number){
		this.clearFormGroup(this.yetkiAddForm);
		this.yetkiService.getYetkiById(yetkiId).subscribe(data=>{
			this.yetki=data;
			this.yetkiAddForm.patchValue(data);
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
