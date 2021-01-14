export const validateFileExtension = (fileName) => {
    const result = {
        isOK: false,
        message: `
        <div>
            <p>The upload file is not correct format, error shall be one of following list:</p>
            <ol>
                <li>Merge column in Excel file</li>
                <li>The file is not excel file</li>
                <li>File has more than 500 rows</li>
            </ol>
        </div >
        `,
    }
    if (!!fileName) {
        const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).toLowerCase();

        const isOK = (fileExtension === "xlsx");

        if (isOK) {
            result.isOK = isOK
            result.message = '';
        }
    }

    return result;
}