import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Proje } from './models/proje';
import { ProjeService } from './services/proje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-proje',
	templateUrl: './proje.component.html',
	styleUrls: ['./proje.component.scss']
})
export class ProjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ustProjeId','adi','aciklama','urlAdresi','bakimUrlAdresi','logo','icon','iconText1','iconText2','iconText3','ico','kullanimKlavuzu','durum', 'update','delete'];

	projeList:Proje[];
	proje:Proje=new Proje();

	projeAddForm: FormGroup;


	projeId:number;

	constructor(private projeService:ProjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getProjeList();
    }

	ngOnInit() {

		this.createProjeAddForm();
	}


	getProjeList() {
		this.projeService.getProjeList().subscribe(data => {
			this.projeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.projeAddForm.valid) {
			this.proje = Object.assign({}, this.projeAddForm.value)

			if (this.proje.id == 0)
				this.addProje();
			else
				this.updateProje();
		}

	}

	addProje(){

		this.projeService.addProje(this.proje).subscribe(data => {
			this.getProjeList();
			this.proje = new Proje();
			jQuery('#proje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.projeAddForm);

		})

	}

	updateProje(){

		this.projeService.updateProje(this.proje).subscribe(data => {

			var index=this.projeList.findIndex(x=>x.id==this.proje.id);
			this.projeList[index]=this.proje;
			this.dataSource = new MatTableDataSource(this.projeList);
            this.configDataTable();
			this.proje = new Proje();
			jQuery('#proje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.projeAddForm);

		})

	}

	createProjeAddForm() {
		this.projeAddForm = this.formBuilder.group({		
			id : [0],
ustProjeId : [0],
adi : ["", Validators.required],
aciklama : ["", Validators.required],
urlAdresi : ["", Validators.required],
bakimUrlAdresi : ["", Validators.required],
logo : ["", Validators.required],
icon : ["", Validators.required],
iconText1 : ["", Validators.required],
iconText2 : ["", Validators.required],
iconText3 : ["", Validators.required],
ico : ["", Validators.required],
kullanimKlavuzu : ["", Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteProje(projeId:number){
		this.projeService.deleteProje(projeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.projeList=this.projeList.filter(x=> x.id!=projeId);
			this.dataSource = new MatTableDataSource(this.projeList);
			this.configDataTable();
		})
	}

	getProjeById(projeId:number){
		this.clearFormGroup(this.projeAddForm);
		this.projeService.getProjeById(projeId).subscribe(data=>{
			this.proje=data;
			this.projeAddForm.patchValue(data);
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
