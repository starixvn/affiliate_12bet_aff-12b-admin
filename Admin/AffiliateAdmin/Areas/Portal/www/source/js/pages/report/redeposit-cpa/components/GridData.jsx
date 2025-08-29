import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { Table } from 'reactstrap';
import { Pager } from '@9softstudio/react-fixed-table-header';
import TableHeader from './table/TableHeader';
import TableBody from './table/TableBody';
import TableFooter from './table/TableFooter';

import { toggleLoader } from '../../../../actions/loaderAction';
import { toggleSubmit, setPagingOption, setParams } from '../actions';
import { getRedepositCpaData } from '../libs/service';

export const GridData = ({ params, isSubmit, toggleLoader, toggleSubmit, setPagingOption, pagingOption, setParams }) => {
    const [data, setData] = useState(null);
    const [total, setTotal] = useState(null);

    useEffect(() => {
        if (isSubmit) {
            fetchData();
        }
    }, [isSubmit]);

    const fetchData = () => {
        toggleLoader(true);
        getRedepositCpaData(params)
            .then(data => {
                if (!!data) {
                    const { MainData, Total, TotalItem } = data;
                    setData(MainData);
                    setTotal(Total);
                    setPagingOption({
                        ...pagingOption,
                        TotalItem: TotalItem
                    });
                }
            })
            .catch(err => {
                console.log('err: ', err);
            })
            .finally(() => {
                toggleLoader(false);
                toggleSubmit(false);
            })
    }

    const handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };
        setPagingOption(newPaging);
        setParams(newPaging);
        toggleSubmit(true);
    }

    return data && (
        <React.Fragment>
            <div className="table-responsive redeposit-cpa-report">
                <Table className="freeze-table table-bordered table-hover">
                    <TableHeader />
                    <TableBody data={data} />
                    <TableFooter total={total} />
                </Table>
            </div>
            <Pager
                pageOption={pagingOption}
                onPaging={handlePaging} />
        </React.Fragment>
    )
};

const mapStateToProps = ({ params, isSubmit, pagingOption }) => {
    return {
        params,
        isSubmit,
        pagingOption,
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        toggleLoader,
        toggleSubmit,
        setPagingOption,
        setParams
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(GridData));