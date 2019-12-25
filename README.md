# EcomEngine
* Sample SOA using .NetCore2.2 with Angular8. 
* Demo app using [Eml*](https://www.nuget.org/packages?q=EddLonzanida) NuGets.
* Check out [EmlExtensions](https://marketplace.visualstudio.com/search?term=EddLonzanida&target=VS&category=Tools&vsVersion=&subCategory=All&sortBy=Relevances) to automate the creation of controllers, views, seeders, and more!.

## Seed the database
1. Open the solution using Visual Studio 2017/2019, compile and build (don't run yet)
2. Right click EcomEngine project and Set as **startup project**
3. Open Package manager console
4. In the 'Default project' **drop down**, select **EcomEngine.DataMigration** (this is important)
5. In the console, type the command below then press enter to execute.
```javascript
Update-Database -verbose -Context EcomEngineDb
```

## Run the application
1. In the Visual Studio 2017/2019 **Standard toolbar**, select **EcomEngine.Api** from the dropdown list, replacing the default IIS Express
2. Press F5 to run the back-end webapi using **Kestrel**
3. Open **Powershell**
4. Navigate to EcomEngine\Hosts\EcomEngine.Spa
5. In the console, type the command below then press enter to execute
```javascript
npm start
```

### If you encounter this error:
```javascript
ERROR in ../node_modules/primeng/components/table/table.d.ts:5:27 - error TS2307: Cannot find module '@angular/core/src/metadata/lifecycle_hooks'.
```

1. Open this file:
**node_modules\primeng\components\table
2. Replace:
```javascript
import { OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
```
3. with:
```javascript
import { OnDestroy } from '@angular/core';
```

### Quick View
![](https://github.com/EddLonzanida/EcomEngine-WebApi/blob/master/Docs/Art/MainScreen.png)