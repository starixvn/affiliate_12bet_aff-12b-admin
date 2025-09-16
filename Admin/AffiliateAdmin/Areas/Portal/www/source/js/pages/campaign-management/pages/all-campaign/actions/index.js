import * as filterAction from './filterAction';
import * as gridAction from './gridAction';
import { setPagingOption } from '@/actions/pagingAction';
import { toggleLoader } from '@/actions/loaderAction';

export const ActionCreators = {
    ...filterAction,
    ...gridAction,
    setPagingOption,
    toggleLoader,
};