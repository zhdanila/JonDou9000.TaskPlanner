using Moq;
using Xunit;
using System;
using System.Linq;
using JonDou9000.TaskPlanner.Domain.JonDou9000.TaskPlanner.Domain.Models;
using JonDou9000.TaskPlanner.DataAccess.JonDou9000.TaskPlanner.DataAccess.Abstractions;

public class SimpleTaskPlannerTests
{
    // 1. �� �������� ���������� ���������� �����
    [Fact]
    public void CreatePlan_ShouldReturnTasksSortedByPriorityAndDueDate()
    {
        // Arrange
        var mockRepository = new Mock<IWorkItemsRepository>();

        // ������ ���
        var tasks = new[]
        {
            new WorkItem { Title = "Task 1", Priority = Priority.Low, DueDate = new DateTime(2024, 1, 10), IsCompleted = false },
            new WorkItem { Title = "Task 2", Priority = Priority.High, DueDate = new DateTime(2024, 1, 5), IsCompleted = false },
            new WorkItem { Title = "Task 3", Priority = Priority.Medium, DueDate = new DateTime(2024, 1, 20), IsCompleted = false },
            new WorkItem { Title = "Task 4", Priority = Priority.Urgent, DueDate = new DateTime(2024, 1, 15), IsCompleted = false }
        };

        mockRepository.Setup(repo => repo.GetAll()).Returns(tasks);

        var planner = new SimpleTaskPlanner(mockRepository.Object);

        // Act
        var result = planner.CreatePlan();

        // Assert
        Assert.Equal(4, result.Length); // ���� 4 ������������ ��������

        // �������� ���������� �� ���������� �� ����� ����������
        Assert.Equal("Task 4", result[0].Title); // Urgent
        Assert.Equal("Task 2", result[1].Title); // High
        Assert.Equal("Task 3", result[2].Title); // Medium
        Assert.Equal("Task 1", result[3].Title); // Low
    }

    // 2. �� �� ��������� (����������) ������ ����������� �� �����
    [Fact]
    public void CreatePlan_ShouldIncludeAllUncompletedTasks()
    {
        // Arrange
        var mockRepository = new Mock<IWorkItemsRepository>();

        // ������ ���, �� ���� ������ ���������
        var tasks = new[]
        {
            new WorkItem { Title = "Task 1", Priority = Priority.Medium, DueDate = new DateTime(2024, 1, 10), IsCompleted = false },
            new WorkItem { Title = "Task 2", Priority = Priority.High, DueDate = new DateTime(2024, 1, 5), IsCompleted = false },
            new WorkItem { Title = "Task 3", Priority = Priority.Medium, DueDate = new DateTime(2024, 1, 20), IsCompleted = true } // ���������
        };

        mockRepository.Setup(repo => repo.GetAll()).Returns(tasks);

        var planner = new SimpleTaskPlanner(mockRepository.Object);

        // Act
        var result = planner.CreatePlan();

        // Assert
        Assert.Equal(2, result.Length); // ���� 2 ������������ ��������

        // ����������, �� �� ���������� �������� �������
        Assert.Contains(result, item => item.Title == "Task 1");
        Assert.Contains(result, item => item.Title == "Task 2");
    }

    // 3. �� �� ������ ���� ����������� (��������) ������
    [Fact]
    public void CreatePlan_ShouldNotIncludeCompletedTasks()
    {
        // Arrange
        var mockRepository = new Mock<IWorkItemsRepository>();

        // ������ ���, �� �� ������ ��������
        var tasks = new[]
        {
            new WorkItem { Title = "Task 1", Priority = Priority.Low, DueDate = new DateTime(2024, 1, 10), IsCompleted = true },
            new WorkItem { Title = "Task 2", Priority = Priority.High, DueDate = new DateTime(2024, 1, 5), IsCompleted = true }
        };

        mockRepository.Setup(repo => repo.GetAll()).Returns(tasks);

        var planner = new SimpleTaskPlanner(mockRepository.Object);

        // Act
        var result = planner.CreatePlan();

        // Assert
        Assert.Empty(result); // ���� �� ������� ������ �������� ������
    }
}
