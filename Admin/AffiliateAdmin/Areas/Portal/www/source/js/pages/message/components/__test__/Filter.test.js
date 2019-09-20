import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

import { DatePicker } from '@9softstudio/react-datepicker';
import { defaultPagingOption } from '../../../../modules/constants';

describe('message - components - Filter ', () => {
    const defaultProps = {
        today: "09/19/2019",
        pagingOption: defaultPagingOption
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ FromDate: "9/19/2019", IsGetAll: true, PageIndex: 1, PageSize: 20, Important: null, ToDate: "9/19/2019" });
    });
})