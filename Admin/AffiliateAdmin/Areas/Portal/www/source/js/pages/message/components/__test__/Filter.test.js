import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../modules/ajaxRequest';
import { DatePicker } from '@9softstudio/react-datepicker';
import { defaultPagingOption } from '../../../../modules/constants';

describe('message - components - Filter ', () => {
    const defaultProps = {
        today: "09/19/2019",
        pagingOption: defaultPagingOption
    }

    it('render correctly with SearchBy All', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });
})