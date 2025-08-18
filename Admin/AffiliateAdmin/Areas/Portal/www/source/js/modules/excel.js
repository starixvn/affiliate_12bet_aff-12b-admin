const EXCEL_TYPE = "xlsx";
const MapExcelType = {
    "number": "n",
    "string": "s",
    "boolean": "b",
    "object": "d"
};

let _XLSX = null;
export default class Excel {
    static async process(callback) {
        try {
            console.log('Loading XLSX library...');
            _XLSX = await import( /* webpackChunkName: "xlsx-style" */ 'xlsx-style-sparta');
            
            if (_XLSX) {
                console.log('XLSX library loaded successfully');
                if (callback) {
                    callback();
                }
            } else {
                throw new Error('XLSX library failed to load');
            }
        } catch (error) {
            console.error('Error loading XLSX library:', error);
            throw error;
        }
    }

    static
        export(worksheet, fileName, sheetName = 'Sheet 1') {
        const option = { bookType: EXCEL_TYPE, bookSST: false, type: 'binary' };
        const workbook = { SheetNames: [sheetName], Sheets: {}, Props: {} };
        workbook.Sheets[sheetName] = worksheet;

        var workbookOutput = _XLSX.write(workbook, option);
        this.download(workbookOutput, fileName);
    }

    static getCellType(value) {
        return MapExcelType[typeof value];
    }

    static getCellAddress(cellObject) {
        return _XLSX.utils.encode_cell(cellObject);
    }

    static download(workbookOut, fileName) {
        try {
            console.log('Creating blob for download...', { fileName, workbookSize: workbookOut.length });
            const arrayBuffer = this.sheetToArrayBuffer(workbookOut);
            const blob = new Blob([arrayBuffer], {
                type: "application/octet-stream"
            });
            
            console.log('Blob created successfully:', { blobSize: blob.size, blobType: blob.type });
            this.downloadFromBolb(blob, fileName);
        } catch (error) {
            console.error('Error creating Excel blob:', error);
            throw error;
        }
    }

    static downloadFromBolb(blob, fileName) {
        const fileNameDownload = `${fileName}.${EXCEL_TYPE}`;
        
        console.log('Starting file download...', { fileNameDownload, blobSize: blob.size });

        try {
            // IE 10+
            if (typeof navigator !== "undefined" && navigator.msSaveOrOpenBlob) {
                console.log('Using IE download method');
                navigator.msSaveOrOpenBlob(blob, fileNameDownload);
            } else {
                console.log('Using standard download method');
                const element = window.document.createElement('a');
                const objectUrl = window.URL.createObjectURL(blob);
                
                element.href = objectUrl;
                element.download = fileNameDownload;
                element.style.display = 'none'; // Hide the element
                
                document.body.appendChild(element);
                element.click();
                
                // Clean up
                document.body.removeChild(element);
                window.URL.revokeObjectURL(objectUrl);
                
                console.log('Download initiated successfully');
            }
        } catch (error) {
            console.error('Error during file download:', error);
            throw error;
        }
    }

    static sheetToArrayBuffer(s) {
        const buffer = new ArrayBuffer(s.length);
        const view = new Uint8Array(buffer);
        for (let i = 0; i <= s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
        return buffer;
    }
}