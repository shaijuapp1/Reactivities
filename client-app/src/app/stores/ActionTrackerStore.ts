import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { v4 as uuid } from 'uuid';
import { ActionTask } from "../models/ActionTask";

export default class ActionTrackerStore {

    activityRegistry = new Map<string, ActionTask>();
    selectedActivity?: ActionTask = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }

    get activitiesByDate() {
        return Array.from(this.activityRegistry.values()).sort((a, b) =>
            Date.parse(a.startDate) - Date.parse(b.startDate))
    }

    get actionTrackerTableArray() {
        return Array.from(this.activityRegistry.values()).sort((a, b) =>
            Date.parse(a.startDate) - Date.parse(b.startDate))
    }

    loadActivities = async () => {
        this.setLoadingInitial(true);
        // try {
        //     const actiontasks = await agent.ActionTasks.list();
        //     actiontasks.forEach(actiontask => {
        //         this.setActivity(actiontask);
        //     })
        //     this.setLoadingInitial(false);
        // } catch (error) {
        //     console.log(error);
        //     this.setLoadingInitial(false);
        // }
    }

    loadActivity = async (id: string) => {
        let actiontask = this.getActivity(id);
        if (actiontask) {
            this.selectedActivity = actiontask;
            return actiontask;
        }
        else {
            // this.setLoadingInitial(true);
            // try {
            //     actiontask = await agent.ActionTasks.details(id);
            //     this.setActivity(actiontask);
            //     runInAction(() => this.selectedActivity = actiontask);
            //     this.setLoadingInitial(false);
            //     return actiontask;
            // } catch (error) {
            //     console.log(error);
            //     this.setLoadingInitial(false);
            // }
        }
    }

    private setActivity = (actiontask: ActionTask) => {
        actiontask.startDate = actiontask.startDate.split('T')[0];
        actiontask.completedDate = actiontask.completedDate.split('T')[0];
        this.activityRegistry.set(actiontask.id, actiontask);
    }

    private getActivity = (id: string) => {
        return this.activityRegistry.get(id);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createActivity = async (actiontask: ActionTask) => {
        this.loading = true;
        actiontask.id = uuid();
        try {
            // await agent.ActionTasks.create(actiontask);
            // runInAction(() => {
            //     this.activityRegistry.set(actiontask.id, actiontask);
            //     this.selectedActivity = actiontask;
            //     this.editMode = false;
            //     this.loading = false;
            // })
        } catch (error) {
            console.log(error);
            runInAction(() => this.loading = false);
        }
    }

    updateActivity = async (actiontask: ActionTask) => {
        this.loading = true;
        // try {
        //     await agent.ActionTasks.update(actiontask)
        //     runInAction(() => {
        //         this.activityRegistry.set(actiontask.id, actiontask);
        //         this.selectedActivity = actiontask;
        //         this.editMode = false;
        //         this.loading = false;
        //     })
        // } catch (error) {
        //     console.log(error);
        //     runInAction(() => this.loading = false);
        // }
    }

    deleteActivity = async (id: string) => {
        this.loading = true;
        // try {
        //     await agent.ActionTasks.delete(id);
        //     runInAction(() => {
        //         this.activityRegistry.delete(id);
        //         this.loading = false;
        //     })
        // } catch (error) {
        //     console.log(error);
        //     runInAction(() => {
        //         this.loading = false;
        //     })
        // }
    }


}
