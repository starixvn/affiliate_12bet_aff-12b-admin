export const handleParseToObject = (data) => {
    const dataObject = data ? JSON.parse(data).root.row : {};

    handleContactApps(dataObject);

    return dataObject;
}

const handleContactApps = (objectData) => {
    const contactApps = objectData['@ContactApps'];

    if (contactApps) {
        const contactAppList = JSON.parse(contactApps);

        delete objectData['@ContactApps'];

        contactAppList.map(item => {
            objectData[`@${item.Name}`] = item.Value;
        });
    }
}