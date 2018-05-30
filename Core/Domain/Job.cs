using System;

namespace JobPortal.Core.Domain
{
    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}

/**
var Job = sequelize.define('Job', {
    title: DataTypes.STRING
  }, {});
  Job.associate = function(models) {
    Job.belongsTo(models.Company, {
      foreignKey: {
        allowNull: false
      }
    });
    Job.belongsToMany(models.Candidate, {
      through: 'Application'
    });
  };
  return Job;
 */