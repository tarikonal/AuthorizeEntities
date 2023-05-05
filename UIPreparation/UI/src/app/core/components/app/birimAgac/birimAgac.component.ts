import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { BirimAgac } from './models/BirimAgac';
import { BirimAgacService } from './services/BirimAgac.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-birimAgac',
	templateUrl: './birimAgac.component.html',
	styleUrls: ['./birimAgac.component.scss']
})
export class BirimAgacComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','birlikId','birimId','uyaptaGorunmeDurumu','durum', 'update','delete'];

	birimAgacList:BirimAgac[];
	birimAgac:BirimAgac=new BirimAgac();

	birimAgacAddForm: FormGroup;


	birimAgacId:number;

	constructor(private birimAgacService:BirimAgacService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getBirimAgacList();
    }

	ngOnInit() {

		this.createBirimAgacAddForm();
	}


	getBirimAgacList() {
		this.birimAgacService.getBirimAgacList().subscribe(data => {
			this.birimAgacList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.birimAgacAddForm.valid) {
			this.birimAgac = Object.assign({}, this.birimAgacAddForm.value)

			if (this.birimAgac.id == 0)
				this.addBirimAgac();
			else
				this.updateBirimAgac();
		}

	}

	addBirimAgac(){

		this.birimAgacService.addBirimAgac(this.birimAgac).subscribe(data => {
			this.getBirimAgacList();
			this.birimAgac = new BirimAgac();
			jQuery('#birimagac').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimAgacAddForm);

		})

	}

	updateBirimAgac(){

		this.birimAgacService.updateBirimAgac(this.birimAgac).subscribe(data => {

			var index=this.birimAgacList.findIndex(x=>x.id==this.birimAgac.id);
			this.birimAgacList[index]=this.birimAgac;
			this.dataSource = new MatTableDataSource(this.birimAgacList);
            this.configDataTable();
			this.birimAgac = new BirimAgac();
			jQuery('#birimagac').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimAgacAddForm);

		})

	}

	createBirimAgacAddForm() {
		this.birimAgacAddForm = this.formBuilder.group({		
			id : [0],
birlikId : [0, Validators.required],
birimId : [0, Validators.required],
uyaptaGorunmeDurumu : [false, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteBirimAgac(birimAgacId:number){
		this.birimAgacService.deleteBirimAgac(birimAgacId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.birimAgacList=this.birimAgacList.filter(x=> x.id!=birimAgacId);
			this.dataSource = new MatTableDataSource(this.birimAgacList);
			this.configDataTable();
		})
	}

	getBirimAgacById(birimAgacId:number){
		this.clearFormGroup(this.birimAgacAddForm);
		this.birimAgacService.getBirimAgacById(birimAgacId).subscribe(data=>{
			this.birimAgac=data;
			this.birimAgacAddForm.patchValue(data);
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
