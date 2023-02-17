import { maxWidth, deviceTypes, minDateOfClickStatistics } from '../../../../modules/constants';
import { headerText } from '../../referrers/libs/constants';
import { globalResource } from '../../../../modules/globalResource';

export { globalResource, deviceTypes, maxWidth, minDateOfClickStatistics }

export const headerTitle = {
    Page: 'Page',
    GroupBy: 'Report By',
    Language: 'Language',
    ...headerText
}

export const groupByCountry = 'Country';

export const groupByList = [groupByCountry, 'Language']
