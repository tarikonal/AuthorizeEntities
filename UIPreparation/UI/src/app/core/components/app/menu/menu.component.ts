import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Menu } from './models/Menu';
import { MenuService } from './services/Menu.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-menu',
	templateUrl: './menu.component.html',
	styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ustMenuId','projeId','adi','aciklama','url','sira','icon','iconText1','iconText2','iconText3','durum', 'update','delete'];

	menuList:Menu[];
	menu:Menu=new Menu();

	menuAddForm: FormGroup;


	menuId:number;

	constructor(private menuService:MenuService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getMenuList();
    }

	ngOnInit() {

		this.createMenuAddForm();
	}


	getMenuList() {
		this.menuService.getMenuList().subscribe(data => {
			this.menuList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.menuAddForm.valid) {
			this.menu = Object.assign({}, this.menuAddForm.value)

			if (this.menu.id == 0)
				this.addMenu();
			else
				this.updateMenu();
		}

	}

	addMenu(){

		this.menuService.addMenu(this.menu).subscribe(data => {
			this.getMenuList();
			this.menu = new Menu();
			jQuery('#menu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.menuAddForm);

		})

	}

	updateMenu(){

		this.menuService.updateMenu(this.menu).subscribe(data => {

			var index=this.menuList.findIndex(x=>x.id==this.menu.id);
			this.menuList[index]=this.menu;
			this.dataSource = new MatTableDataSource(this.menuList);
            this.configDataTable();
			this.menu = new Menu();
			jQuery('#menu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.menuAddForm);

		})

	}

	createMenuAddForm() {
		this.menuAddForm = this.formBuilder.group({		
			id : [0],
ustMenuId : [0, Validators.required],
projeId : [0, Validators.required],
adi : ["", Validators.required],
aciklama : ["", Validators.required],
url : ["", Validators.required],
sira : [0, Validators.required],
icon : ["", Validators.required],
iconText1 : ["", Validators.required],
iconText2 : ["", Validators.required],
iconText3 : ["", Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteMenu(menuId:number){
		this.menuService.deleteMenu(menuId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.menuList=this.menuList.filter(x=> x.id!=menuId);
			this.dataSource = new MatTableDataSource(this.menuList);
			this.configDataTable();
		})
	}

	getMenuById(menuId:number){
		this.clearFormGroup(this.menuAddForm);
		this.menuService.getMenuById(menuId).subscribe(data=>{
			this.menu=data;
			this.menuAddForm.patchValue(data);
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
