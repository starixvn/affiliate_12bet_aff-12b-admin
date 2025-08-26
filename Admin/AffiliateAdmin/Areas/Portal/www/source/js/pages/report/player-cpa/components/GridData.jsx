import React from 'react'
import { connect } from 'react-redux';
import { Fragment } from 'react';
import { Table } from 'reactstrap';
import TableHeader from './table/TableHeader';
import usePlayerCpaData from '../hooks/usePlayerCpaData';
import { Pager } from '@9softstudio/react-fixed-table-header';
import { getMaxHeight } from '../../libs/constants';
import TableBody from './table/TableBody';
import { setParams, toggleSubmit, setPagingOption } from '../actions';
import TableFooter from './table/TableFooter';
import TurnoverDetail from './turnover/TurnoverDetail';

const GridData = ({ params, pagingOption, isSubmit, memberCpaId, dispatch }) => {
    const { playerCpaData, turnoverDetail } = usePlayerCpaData(isSubmit, params, pagingOption, memberCpaId, dispatch)
    const maxHeight = getMaxHeight()

    const handlePaging = (pageIndex, pageSize) => {
        dispatch(setParams({ ...params, pageIndex: pageIndex, pageSize: pageSize}));
        dispatch(setPagingOption({
            ...pagingOption,
            PageIndex: pageIndex,
            PageSize: pageSize
        }));
        dispatch(toggleSubmit());
    }

    if(!playerCpaData) {
        return null
    }

    return (
        <Fragment>
            <div className="table-responsive" style={{ maxHeight }}>
                <Table className="freeze-table table-bordered table-hover">
                    <TableHeader 
                        sortColumn={params?.sortColumn} 
                        isAscending={params?.isAscending} 
                    />
                    <TableBody 
                        data={playerCpaData?.mainData} 
                        pagingOption={pagingOption} />
                    <TableFooter 
                        total={playerCpaData?.total}
                        totalItem={playerCpaData?.totalItem}/>
                </Table>
            </div>  
            <Pager
                pageOption={pagingOption}
                onPaging={handlePaging} />
            <TurnoverDetail turnoverDetail={turnoverDetail} />
        </Fragment>
    )
}

const mapStateToProps = (state) => {
    return {
        isSubmit: state.isSubmit,
        params: state.params,
        pagingOption: state.pagingOption,
        memberCpaId: state.memberCpaId
    }
}

export default connect(mapStateToProps)(React.memo(GridData));