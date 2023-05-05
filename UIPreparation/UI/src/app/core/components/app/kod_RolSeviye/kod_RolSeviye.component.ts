import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Kod_RolSeviye } from './models/Kod_RolSeviye';
import { Kod_RolSeviyeService } from './services/Kod_RolSeviye.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kod_RolSeviye',
	templateUrl: './kod_RolSeviye.component.html',
	styleUrls: ['./kod_RolSeviye.component.scss']
})
export class Kod_RolSeviyeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','seviyeKodu', 'update','delete'];

	kod_RolSeviyeList:Kod_RolSeviye[];
	kod_RolSeviye:Kod_RolSeviye=new Kod_RolSeviye();

	kod_RolSeviyeAddForm: FormGroup;


	kod_RolSeviyeId:number;

	constructor(private kod_RolSeviyeService:Kod_RolSeviyeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKod_RolSeviyeList();
    }

	ngOnInit() {

		this.createKod_RolSeviyeAddForm();
	}


	getKod_RolSeviyeList() {
		this.kod_RolSeviyeService.getKod_RolSeviyeList().subscribe(data => {
			this.kod_RolSeviyeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kod_RolSeviyeAddForm.valid) {
			this.kod_RolSeviye = Object.assign({}, this.kod_RolSeviyeAddForm.value)

			if (this.kod_RolSeviye.id == 0)
				this.addKod_RolSeviye();
			else
				this.updateKod_RolSeviye();
		}

	}

	addKod_RolSeviye(){

		this.kod_RolSeviyeService.addKod_RolSeviye(this.kod_RolSeviye).subscribe(data => {
			this.getKod_RolSeviyeList();
			this.kod_RolSeviye = new Kod_RolSeviye();
			jQuery('#kod_rolseviye').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kod_RolSeviyeAddForm);

		})

	}

	updateKod_RolSeviye(){

		this.kod_RolSeviyeService.updateKod_RolSeviye(this.kod_RolSeviye).subscribe(data => {

			var index=this.kod_RolSeviyeList.findIndex(x=>x.id==this.kod_RolSeviye.id);
			this.kod_RolSeviyeList[index]=this.kod_RolSeviye;
			this.dataSource = new MatTableDataSource(this.kod_RolSeviyeList);
            this.configDataTable();
			this.kod_RolSeviye = new Kod_RolSeviye();
			jQuery('#kod_rolseviye').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kod_RolSeviyeAddForm);

		})

	}

	createKod_RolSeviyeAddForm() {
		this.kod_RolSeviyeAddForm = this.formBuilder.group({		
			id : [0],
seviyeKodu : [0, Validators.required]
		})
	}

	deleteKod_RolSeviye(kod_RolSeviyeId:number){
		this.kod_RolSeviyeService.deleteKod_RolSeviye(kod_RolSeviyeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kod_RolSeviyeList=this.kod_RolSeviyeList.filter(x=> x.id!=kod_RolSeviyeId);
			this.dataSource = new MatTableDataSource(this.kod_RolSeviyeList);
			this.configDataTable();
		})
	}

	getKod_RolSeviyeById(kod_RolSeviyeId:number){
		this.clearFormGroup(this.kod_RolSeviyeAddForm);
		this.kod_RolSeviyeService.getKod_RolSeviyeById(kod_RolSeviyeId).subscribe(data=>{
			this.kod_RolSeviye=data;
			this.kod_RolSeviyeAddForm.patchValue(data);
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
