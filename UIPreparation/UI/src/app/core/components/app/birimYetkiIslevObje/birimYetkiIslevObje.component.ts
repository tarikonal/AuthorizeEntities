import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { BirimYetkiIslevObje } from './models/BirimYetkiIslevObje';
import { BirimYetkiIslevObjeService } from './services/BirimYetkiIslevObje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-birimYetkiIslevObje',
	templateUrl: './birimYetkiIslevObje.component.html',
	styleUrls: ['./birimYetkiIslevObje.component.scss']
})
export class BirimYetkiIslevObjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','birimId','yetkiId','islevId','objeId','durum', 'update','delete'];

	birimYetkiIslevObjeList:BirimYetkiIslevObje[];
	birimYetkiIslevObje:BirimYetkiIslevObje=new BirimYetkiIslevObje();

	birimYetkiIslevObjeAddForm: FormGroup;


	birimYetkiIslevObjeId:number;

	constructor(private birimYetkiIslevObjeService:BirimYetkiIslevObjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getBirimYetkiIslevObjeList();
    }

	ngOnInit() {

		this.createBirimYetkiIslevObjeAddForm();
	}


	getBirimYetkiIslevObjeList() {
		this.birimYetkiIslevObjeService.getBirimYetkiIslevObjeList().subscribe(data => {
			this.birimYetkiIslevObjeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.birimYetkiIslevObjeAddForm.valid) {
			this.birimYetkiIslevObje = Object.assign({}, this.birimYetkiIslevObjeAddForm.value)

			if (this.birimYetkiIslevObje.id == 0)
				this.addBirimYetkiIslevObje();
			else
				this.updateBirimYetkiIslevObje();
		}

	}

	addBirimYetkiIslevObje(){

		this.birimYetkiIslevObjeService.addBirimYetkiIslevObje(this.birimYetkiIslevObje).subscribe(data => {
			this.getBirimYetkiIslevObjeList();
			this.birimYetkiIslevObje = new BirimYetkiIslevObje();
			jQuery('#birimyetki?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimYetkiIslevObjeAddForm);

		})

	}

	updateBirimYetkiIslevObje(){

		this.birimYetkiIslevObjeService.updateBirimYetkiIslevObje(this.birimYetkiIslevObje).subscribe(data => {

			var index=this.birimYetkiIslevObjeList.findIndex(x=>x.id==this.birimYetkiIslevObje.id);
			this.birimYetkiIslevObjeList[index]=this.birimYetkiIslevObje;
			this.dataSource = new MatTableDataSource(this.birimYetkiIslevObjeList);
            this.configDataTable();
			this.birimYetkiIslevObje = new BirimYetkiIslevObje();
			jQuery('#birimyetki?slevobje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimYetkiIslevObjeAddForm);

		})

	}

	createBirimYetkiIslevObjeAddForm() {
		this.birimYetkiIslevObjeAddForm = this.formBuilder.group({		
			id : [0],
birimId : [0, Validators.required],
yetkiId : [0, Validators.required],
islevId : [0, Validators.required],
objeId : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteBirimYetkiIslevObje(birimYetkiIslevObjeId:number){
		this.birimYetkiIslevObjeService.deleteBirimYetkiIslevObje(birimYetkiIslevObjeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.birimYetkiIslevObjeList=this.birimYetkiIslevObjeList.filter(x=> x.id!=birimYetkiIslevObjeId);
			this.dataSource = new MatTableDataSource(this.birimYetkiIslevObjeList);
			this.configDataTable();
		})
	}

	getBirimYetkiIslevObjeById(birimYetkiIslevObjeId:number){
		this.clearFormGroup(this.birimYetkiIslevObjeAddForm);
		this.birimYetkiIslevObjeService.getBirimYetkiIslevObjeById(birimYetkiIslevObjeId).subscribe(data=>{
			this.birimYetkiIslevObje=data;
			this.birimYetkiIslevObjeAddForm.patchValue(data);
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
